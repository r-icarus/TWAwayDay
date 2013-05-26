using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceBeans {
    internal class GamePlayer {
        private readonly SpaceBeansGame game;
        private readonly ConsolePlayer[] players;

        public GamePlayer(SpaceBeansGame game, IEnumerable<ConsolePlayer> players) {
            this.game = game;
            this.players = players.ToArray();
        }

        public void PlayGame() {
            game.Start();
            while (!game.IsOver) {
                foreach(var decision in game.Decisions.ToArray()) {
                    players.First(p => decision.Trader == p.Trader).MakeDecision(decision);
                }
            }
            Console.Clear();
            Console.WriteLine("{0} wins with a score of {1}!", game.Winner.Name, game.Winner.TrophyPoints);
            Console.ReadLine();
        }
    }
}