using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using CodePhile.Games;
using CodePhile.ServiceModel;

namespace SpaceBeans {
    [ConsoleErrorHandler]
    public class SpaceBeansGameClient : GameClient, ISpaceBeansGameClient, IDisposable {

        private readonly DuplexClientProxy<ISpaceBeansGameHost> _proxy;
        private readonly IList<ConsolePlayer> players = new List<ConsolePlayer>();

        public SpaceBeansGameClient() {
            _proxy = new DuplexClientProxy<ISpaceBeansGameHost>(this, new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:9000"));
        }

        public IEnumerable<ConsolePlayer> Players {
            get { return players; } // TODO readonly
        }

        public SpaceBeansGame JoinGame() {
            _proxy.Open();
            var seat = (SpaceBeansSeat)_proxy.Channel.GetOpenSeats().First();
            _proxy.Channel.JoinGame(seat);
            var setup = new SpaceBeansGameSetup();
            setup.AddTrader(CreateRemoteTrader(0));
            setup.AddTrader(CreateLocalTrader(1));
            var game = new SpaceBeansGame(setup);
            game.AddResultSource(this);
            game.DecisionNeeded += (o, args) => args.Decision.Decided += (sender, result) => {
                                                                                                   if (!DecisionResolved(result)) {
                                                                                                       _proxy.Channel.ResolveDecision(result);
                                                                                                   }
            };
            return game;
        }

        private Trader CreateLocalTrader(int position) {
            var player = new LocalPlayer(position);
            players.Add(player);
            return player.Trader;
        }

        private Trader CreateRemoteTrader(int position) {
            var player = new RemotePlayer(position, _proxy.Channel);
            players.Add(player);
            return player.Trader;
        }

        private readonly object waitToResolve = new object();
        protected override void OnDecisionResultReceived(DecisionResult result) {
            lock (waitToResolve) {
                base.OnDecisionResultReceived(result);
            }
        }

        public override Decision FindDecisionForResult(DecisionResult result, Game game) {
            return game.Decisions.First();
        }

        public void Dispose() {
            if (null != _proxy) {
                try {
                    _proxy.Dispose();
                } catch (CommunicationException ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}