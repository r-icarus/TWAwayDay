using System;
using System.Collections.Generic;
using System.Linq;

using CodePhile.Games;

namespace SpaceBeans {
    public class SpaceBeansGame : Game {
        public SpaceBeansGame() : base(new SpaceBeansGamePart(2)) {}

        public Turn CurrentTurn {
            get { return ((Turn)GamePart.CurrentGamePart); }
        }

        private new SpaceBeansGamePart GamePart {
            get { return (SpaceBeansGamePart)base.GamePart; }
        }

        public Trader Winner {
            get { return GamePart.Winner; }
        }

        private class SpaceBeansGamePart : IterativeParentGamePart {

            private readonly Trader[] traders;
            private readonly DiscardPile discardPile;
            private readonly DrawPile drawPile;

            public SpaceBeansGamePart(int playerCount) {
                discardPile = new DiscardPile();
                drawPile = new DrawPile(discardPile);

                this.traders = Enumerable.Range(0, playerCount).Select(i => new Trader("Player " + (i + 1))).ToArray();
                var suits = (Suit[])Enum.GetValues(typeof(Suit));
                var allCards = (
                                   from s in suits
                                   from r in Enumerable.Range(1, 3)
                                   select new Bean(r, s)
                               ).Concat(
                                   from s in suits
                                   from r in Enumerable.Range(4, 6)
                                   from x in Enumerable.Range(0, 2)
                                   select new Bean(r, s)
                               );
                allCards = Randomize(allCards);
                drawPile.AddBeans(allCards);
            }

            private static IList<T> Randomize<T>(IEnumerable<T> source) {
                List<T> randomized = new List<T>();
                Random rand = new Random();
                foreach(var item in source) {
                    randomized.Insert(rand.Next(randomized.Count), item);
                }
                return randomized;
            }

            protected override GamePart GetGamePart(int iteration) {
                return GameIsOver() ? null : new Turn(GetPlayerAt(iteration), drawPile, discardPile, GetPlayerAt(iteration - 1));
            }

            private Trader GetPlayerAt(int index) {
                return traders[(index + traders.Length) % traders.Length];
            }

            private bool GameIsOver() {
                var winner = traders.FirstOrDefault(p => p.TrophyPoints > 1);
                if(null != winner) {
                    winner.IsWinner = true;
                    foreach(var trader in traders) {
                        trader.SellRevealedCollection();
                        trader.SellRevealedCollection();
                    }
                }
                return winner != null;
            }

            public Trader Winner {
                get {
                    // TODO: handle ties
                    return traders
                        // TODO: extract MaxBy
                            .OrderByDescending(t => t.TrophyPoints)
                            .First();
                }
            }
        }
    }
}
