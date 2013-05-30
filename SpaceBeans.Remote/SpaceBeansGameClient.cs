using System;
using System.Linq;
using System.ServiceModel;
using System.Threading;

using CodePhile.Games;
using CodePhile.ServiceModel;

namespace SpaceBeans {
    [ConsoleErrorHandler]
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Single)]
    public abstract class SpaceBeansGameClient : GameClient, ISpaceBeansGameClient, IDisposable {

        private DuplexClientProxy<ISpaceBeansGameHost> proxy;
        private readonly SpaceBeansGameSetup setup = new SpaceBeansGameSetup();
        private SpaceBeansGame game;
        private readonly ManualResetEvent waitForGameStart = new ManualResetEvent(false);
        private readonly ManualResetEvent waitForReadyToStart = new ManualResetEvent(false);

        public SpaceBeansGameClient(Func<ISpaceBeansGameClient, DuplexClientProxy<ISpaceBeansGameHost>> hostProxyFactory) {
            proxy = hostProxyFactory(this);
        }

        public SpaceBeansGame Game {
            get { return game; }
        }

        public void JoinGameServer() {
            bool joined = false;
            SpaceBeansSeat mySeat;
            do {
                var seats = proxy.Channel.GetOpenSeats();
                mySeat = (SpaceBeansSeat)seats.First();
                try {
                    proxy.Channel.JoinGame(mySeat);
                    joined = true;
                } catch (FaultException) {
                    // try again;
                }
            } while (!joined);

            var trader = GetLocalTrader(mySeat);
            setup.AddTrader(trader);
            waitForReadyToStart.Set();

            OnWaitingForGameStart();
            waitForGameStart.WaitOne();
        }

        protected abstract Trader GetLocalTrader(SpaceBeansSeat seat);

        public event EventHandler WaitingForGameStart;

        private void OnWaitingForGameStart() {
            var handler = WaitingForGameStart;
            if(null != handler) {
                handler(this, EventArgs.Empty);
            }
        }

        public void PlayerJoined(SpaceBeansSeat seat, string name) {
            var trader = GetRemoteTrader(seat, name);
            setup.AddTrader(trader);
        }

        protected abstract Trader GetRemoteTrader(SpaceBeansSeat seat, string name);

        public void GameStarted() {
            waitForReadyToStart.WaitOne();
            game = new SpaceBeansGame(setup);
            game.AddResultSource(this);
            game.DecisionNeeded += (sender, args) => {
                    args.Decision.Decided += (decision, result) => {
                            if (!DecisionResolved(result)) {
                                proxy.Channel.ResolveDecision(result);
                            }
                        };
                };
            game.Start();
            waitForGameStart.Set();
        }

        public override Decision FindDecisionForResult(DecisionResult result, Game game) {
            return game.Decisions.First();
        }

        public void Dispose() {
            if (null != proxy) {
                try {
                    proxy.Dispose();
                } catch (CommunicationException ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}