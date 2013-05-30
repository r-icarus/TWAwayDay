using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading.Tasks;

using CodePhile.Games;

namespace SpaceBeans {
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ConsoleErrorHandler]
    public class SpaceBeansGameHost : GameHost, ISpaceBeansGameHost {
        private const int MaximumPlayerCount = 2;
        private readonly SpaceBeansGameSetup setup = new SpaceBeansGameSetup();
        private readonly IDictionary<string, ISpaceBeansGameClient> players = new Dictionary<string, ISpaceBeansGameClient>();

        private int OpenSeatCount {
            get { return MaximumPlayerCount - setup.TraderCount; }
        }

        public override GameSeat[] GetOpenSeats() {
            return Enumerable.Range(setup.TraderCount, OpenSeatCount)
                             .Select(i => new SpaceBeansSeat {
                                 Position = i
                             }).ToArray();
        }

        public override void JoinGame(GameSeat seat) {
            var spaceBeansSeat = (SpaceBeansSeat)seat;
            if(setup.TraderCount > spaceBeansSeat.Position) {
                throw new FaultException("Seat already taken.");
            }

            var newTrader = new Trader(OperationContext.Current.SessionId);
            setup.AddTrader(newTrader);
            var oldPlayers = players.Values.ToArray();
            var newCallback = OperationContext.Current.GetCallbackChannel<ISpaceBeansGameClient>();
            players.Add(newTrader.Name, newCallback);
            foreach (var client in oldPlayers) {
                client.PlayerJoined(spaceBeansSeat, newTrader.Name);
            }
            Task.Factory.StartNew(() =>
                {
                    for(int traderIndex = 0; traderIndex < setup.GetTraders().Length; traderIndex++) {
                        var trader = setup.GetTraders()[traderIndex];
                        if(newTrader != trader) {
                            newCallback.PlayerJoined(new SpaceBeansSeat {
                                Position = traderIndex
                            }, trader.Name);
                        }
                    }
                    if(OpenSeatCount == 0) {
                        Task.Factory.StartNew(StartGame);
                    }
                });
        }

        protected override void OnDecisionResultReceived(DecisionResult result) {
            foreach (var sessionId in IEnumerableExtensions.Except(players.Keys, OperationContext.Current.SessionId)) {
                players[sessionId].ResolveDecision(result);
            }
            base.OnDecisionResultReceived(result);
        }

        public override Decision FindDecisionForResult(DecisionResult result, Game game) {
            return game.Decisions.First();
        }

        public void StartGame() {
            foreach (var player in players.Values) {
                player.GameStarted();
            }
            SpaceBeansGame game = new SpaceBeansGame(setup);
            game.AddResultSource(this);
            game.DecisionNeeded += (o, args) => {
                                                    args.Decision.Decided += (sender, result) => {
                                                                                                     if (!DecisionResolved(result)) {
                                                                                                         foreach (var player in players.Values) {
                                                                                                             player.ResolveDecision(result);
                                                                                                         }
                                                                                                     }
                                                    };
                                                    if (((ISpaceBeansDecision)args.Decision).Trader == null) {
                                                        var allCards = StandardRules.GenerateStandardCards();
                                                        allCards = Randomize(allCards);
                                                        ((SetupDrawPileDecision)args.Decision).AddBeans(allCards);
                                                    }
            };
            game.Start();
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

        public static class IEnumerableExtensions {
            public static IEnumerable<T> Except<T>(IEnumerable<T> source, T item) {
                return source.Except(Enumerable.Repeat(item, 1));
            }
        }
    }
}