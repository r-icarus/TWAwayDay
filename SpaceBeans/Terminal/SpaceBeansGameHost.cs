using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;

using CodePhile.Games;

namespace SpaceBeans {
    [ConsoleErrorHandler]
    public class SpaceBeansGameHost : GameHost, ISpaceBeansGameHost {

        private readonly List<ConsolePlayer> players = new List<ConsolePlayer>();

        public SpaceBeansGameHost() {
            players.Add(new LocalPlayer(0));
        }

        public SpaceBeansGame Game {
            get {
                var setup = new SpaceBeansGameSetup();
                foreach(var player in players) {
                    setup.AddTrader(player.Trader);
                }
                var game = new SpaceBeansGame(setup);
                game.AddResultSource(this);
                game.DecisionNeeded += (o, args) => args.Decision.Decided += (sender, result) => {
                        if (!DecisionResolved(result)) {
                            foreach (var remotePlayer in players.OfType<RemotePlayer>()) {
                                remotePlayer.ResolveDecision(result);
                            }
                        }
                    };
                return game;
            }
        }

        public IEnumerable<ConsolePlayer> Players {
            get { return players; } // TODO return readonly
        }

        private readonly object waitToResolve = new object();
        protected override void OnDecisionResultReceived(DecisionResult result) {
            lock(waitToResolve) {
                base.OnDecisionResultReceived(result);
            }
        }

        public override Decision FindDecisionForResult(DecisionResult result, Game game) {
            return game.Decisions.First();
        }

        public override GameSeat[] GetOpenSeats() {
            return Enumerable.Range(players.Count, players.Capacity - players.Count)
                .Select(i => new SpaceBeansSeat {
                                    Position = i
                                 }
                ).ToArray();
        }

        public override void JoinGame(GameSeat seat) {
            var remotePlayer = new RemotePlayer(((SpaceBeansSeat)seat).Position, OperationContext.Current.GetCallbackChannel<ISpaceBeansGameClient>());
            players.Add(remotePlayer);
            OnPlayerJoined(remotePlayer);
            base.JoinGame(seat);
        }
    }
}
