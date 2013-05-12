using System.Collections.Generic;
using System.Linq;

using CodePhile.Games;

namespace SpaceBeans {
    internal class PlayTurnsGamePart : IterativeParentGamePart {

        private readonly Trader[] traders;
        private readonly DiscardPile discardPile;
        private readonly DrawPile drawPile;

        public PlayTurnsGamePart(IEnumerable<Trader> traders, DiscardPile discardPile, DrawPile drawPile) {
            this.discardPile = discardPile;
            this.drawPile = drawPile;

            this.traders = traders.ToArray();
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
            }
            return winner != null;
        }

        protected override GamePart End() {
            foreach (var trader in traders) {
                trader.SellRevealedCollection();
                trader.SellRevealedCollection();
            }
            return null;
        }
    }
}