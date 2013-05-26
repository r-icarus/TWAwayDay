using System;
using System.Collections.Generic;
using System.Linq;

using CodePhile.ServiceModel;

namespace SpaceBeans {
    class SpaceBeansTerminalClient : SpaceBeansGameClient {
        private readonly IDictionary<int, ConsolePlayer> players = new Dictionary<int, ConsolePlayer>();

        public SpaceBeansTerminalClient(Func<ISpaceBeansGameClient, DuplexClientProxy<ISpaceBeansGameHost>> hostProxyFactory) : base(hostProxyFactory) {}

        public IEnumerable<ConsolePlayer> Players {
            get { return players.OrderBy(p => p.Key).Select(p => p.Value); }
        }

        protected override Trader GetLocalTrader(SpaceBeansSeat seat) {
            var me = new LocalPlayer(seat.Position);
            players[seat.Position] = me;
            return me.Trader;
        }

        protected override Trader GetRemoteTrader(SpaceBeansSeat seat, string name) {
            var remotePlayer = new RemotePlayer(name);
            players[seat.Position] = remotePlayer;
            return remotePlayer.Trader;
        }
    }
}
