using System.Collections.Generic;
using System.Linq;

using CodePhile;
using CodePhile.Games;

namespace SpaceBeans {
    public class BuyDecision : Decision {
        private readonly Trader trader;

        public BuyDecision(Trader trader) {
            this.trader = trader;
        }

        public IEnumerable<Bean> AvailableCards {
            get { return trader.CardsInHand; }
        }

        public void PlayCards(IEnumerable<Bean> cardsToPlay, CollectionIdentifier targetCollection) {
            OnCompleted(new PlayCardsDecisionResult(cardsToPlay, targetCollection));
        }

        private class PlayCardsDecisionResult : DecisionResult<BuyDecision> {
            private readonly ISet<Bean> cardsToPlay;
            private readonly CollectionIdentifier targetCollection;

            public PlayCardsDecisionResult(IEnumerable<Bean> cardsToPlay, CollectionIdentifier targetCollection) {
                this.cardsToPlay = cardsToPlay.ToSet();
                this.targetCollection = targetCollection;
            }

            public override void Validate(BuyDecision decision) {
                if(cardsToPlay.Count == 0) {
                    throw new RuleViolationException("Must play at least one card.");
                }
                if(cardsToPlay.Except(decision.AvailableCards).Any()) {
                    throw new RuleViolationException("All cards played must come from player's hand.");
                }
                var cardsBySuit = cardsToPlay.GroupBy(c => c.Suit).ToArray();
                if(cardsBySuit.Length != 1) {
                    throw new RuleViolationException("All cards played must be of the same suit.");
                }
                if(!GetTargetCollection(decision).CanAddBean(cardsToPlay.First())) { // HACK: only test one card?
                    throw new RuleViolationException("Each collection may only hold cards of one suit.");
                }
            }

            public override void Resolve(BuyDecision decision) {
                decision.trader.PlayCardsToCollection(targetCollection, cardsToPlay);
            }

            private BeanCargo GetTargetCollection(BuyDecision decision) {
                return decision.trader.GetCollection(targetCollection);
            }
        }
    }
}