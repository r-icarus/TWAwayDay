using System;
using System.Collections.Generic;
using System.Linq;

using CodePhile;
using CodePhile.Terminal;

namespace SpaceBeans {
    class Program {
        static void Main() {
            Action choice = null;
            do {
                if (null != choice) {
                    choice();
                }
                var menu = new Menu {
                    Labels = new LabelSequence[] { NumericLabelSequence.Instance, },
                };
                menu.Items.Add(new MenuItem { Text = "PvP", State = new Action(LocalPlayerVersusLocalPlayer), });
                menu.Items.Add(new MenuItem { Text = "Quit", });
                Console.Clear();
                choice = (Action)menu.Choose().State;
            }
            while (null != choice);
        }

        private static void LocalPlayerVersusLocalPlayer() {
            var game = new SpaceBeansGame();
            PlayGame(game);
        }

        private static IEnumerable<IDecisionMaker> GetDecisionMakers(Trader player) {
            yield return new DrawDecisionMaker(player);
            yield return new SellDecisionMaker(player);
            yield return new BuyDecisionMaker(player);
        }

       private static void PlayGame(SpaceBeansGame game) {
            game.Start();
            while (!game.IsOver) {
                foreach (var decision in game.Decisions.ToArray()) {
                    ForEachUntil(GetDecisionMakers(game.CurrentTurn.Player), m => m.MakeDecision(decision));
                }
            }
           Console.Clear();
           Console.WriteLine("{0} wins with a score of {1}!", game.Winner.Name, game.Winner.TrophyPoints);
           Console.ReadLine();
       }

        public static void ForEachUntil<T>(IEnumerable<T> decisionMakers, Func<T, bool> predicate) {
            // TODO: improve this
            decisionMakers.TakeUntil(predicate).ToArray();
        }
    }
}
