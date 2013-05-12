using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;

using CodePhile;
using CodePhile.Games;
using CodePhile.Terminal;

namespace SpaceBeans {
    internal class Program {
        private static void Main() {
            Action choice = null;
            do {
                if(null != choice) {
                    choice();
                }
                var menu = new Menu {
                    Labels = new LabelSequence[] {NumericLabelSequence.Instance,},
                };
                menu.Items.Add(new MenuItem {
                    Text = "PvP",
                    State = new Action(LocalPlayerVersusLocalPlayer),
                });
                menu.Items.Add(new MenuItem {
                    Text = "Host Game",
                    State = new Action(HostGame),
                });
                menu.Items.Add(new MenuItem {
                    Text = "Join Game",
                    State = new Action(JoinGame),
                });
                menu.Items.Add(new MenuItem {
                    Text = "Quit",
                });
                Console.Clear();
                choice = menu.Choose<Action>();
            } while(null != choice);
        }

        private static void HostGame() {
            var host = new SpaceBeansGameHost();
            var playerJoined = false;
            host.PlayerJoined += player => playerJoined = true;
            try {
                using(var serviceHost = new ServiceHost(host)) {
                    serviceHost.AddServiceEndpoint(typeof(ISpaceBeansGameHost), new NetTcpBinding(), "net.tcp://localhost:9000");
                    serviceHost.Open();
                    Console.WriteLine("Waiting for other player to join (press any key to cancel)...");
                    while(!playerJoined && !Console.KeyAvailable) {
                        Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }
                    if(!playerJoined) {
                        Console.ReadKey(true);
                    } else {
                        new GamePlayer(host.Game, host.Players.Append(new LocalSystem())).PlayGame();
                    }
                    Console.WriteLine("Closing hosted game...");
                    serviceHost.Close();
                }
            } catch(CommunicationException ex) {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private static void JoinGame() {
            try {
                using(var client = new SpaceBeansGameClient()) {
                    Console.WriteLine("Joining game...");
                    var game = client.JoinGame();
                    new GamePlayer(game, client.Players.Append(new RemoteSystem())).PlayGame();
                }
            } catch(CommunicationException ex) {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private static void LocalPlayerVersusLocalPlayer() {
            var setup = new SpaceBeansGameSetup();
            IEnumerable<ConsolePlayer> players = Enumerable.Range(1, 2).Select(i => new LocalPlayer(i)).ToArray();
            foreach(var trader in players.Select(p => p.Trader)) {
                setup.AddTrader(trader);
            }
            var game = new SpaceBeansGame(setup);
            new GamePlayer(game, players.Append(new LocalSystem())).PlayGame();
        }

        private class GamePlayer {
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

        private class RemoteSystem : ConsolePlayer {
            public override void MakeDecision(ISpaceBeansDecision decision) {
                Console.Clear();
                Console.WriteLine("Waiting for system...");
                ((Decision)decision).WaitUntilComplete();
            }
        }

        private class LocalSystem : ConsolePlayer {
            public override void MakeDecision(ISpaceBeansDecision decision) {
                ForEachUntil(GetDecisionMakers(), m => m.MakeDecision(decision));
            }

            private IEnumerable<IDecisionMaker> GetDecisionMakers() {
                yield return new GameSetupDecisionMaker();
            }
        }
    }

    internal class GameSetupDecisionMaker : DecisionMaker<SetupDrawPileDecision> {
        public override void MakeDecision(SetupDrawPileDecision decision) {
            var allCards = StandardRules.GenerateStandardCards();
            allCards = Randomize(allCards);
            decision.AddBeans(allCards);
        }

        private static IEnumerable<T> Randomize<T>(IEnumerable<T> source) {
            var sourceCopy = source.ToList();
            var rand = new Random();
            while (sourceCopy.Count > 0) {
                var nextIndex = rand.Next(0, sourceCopy.Count - 1);
                yield return sourceCopy[nextIndex];
                sourceCopy.RemoveAt(nextIndex);
            }
        }
    }
}