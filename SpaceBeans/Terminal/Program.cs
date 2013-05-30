using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;

using CodePhile;
using CodePhile.ServiceModel;
using CodePhile.Terminal;

using Microsoft.Samples.DuplexHttp;

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

        private static void JoinGame() {
            try {
                var binding = new DuplexHttpBinding {
                    PollingInterval = TimeSpan.FromSeconds(1),
                    PollingReplyInterval = TimeSpan.FromMinutes(10),
                };
                using(var client = new SpaceBeansTerminalClient(instance => new DuplexClientProxy<ISpaceBeansGameHost>(instance, "spaceBeans"))) {
                    client.WaitingForGameStart += (sender, args) => Console.WriteLine("Waiting for game start...");
                    Console.WriteLine("Joining game...");
                    client.JoinGameServer();
                    new GamePlayer(client.Game, client.Players.Append(RemoteSystem.Instance)).PlayGame();
                }
            } catch (CommunicationException ex) {
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
            game.Start();
            new GamePlayer(game, players.Append(new LocalSystem())).PlayGame();
        }
    }
}