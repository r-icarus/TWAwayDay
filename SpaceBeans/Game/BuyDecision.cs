using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using CodePhile;
using CodePhile.Games;

namespace SpaceBeans {
    public class BuyDecision : Decision, ISpaceBeansDecision {

        private readonly Trader trader;

        public BuyDecision(Trader trader) {
            this.trader = trader;
        }

        public Trader Trader {
            get { return trader; }
        }

        public IEnumerable<Bean> AvailableBeans {
            get { return Trader.BeansInHand; }
        }

        public void PlayCards(IEnumerable<Bean> cardsToPlay, CollectionIdentifier targetCollection) {
            OnDecided(new BuyBeansResult(cardsToPlay, targetCollection));
        }

        [DataContract]
        public class BuyBeansResult : DecisionResult<BuyDecision> {
            [DataMember]
            private readonly ISet<Bean> cardsToPlay;
            [DataMember]
            private readonly CollectionIdentifier targetCollection;

            public BuyBeansResult(IEnumerable<Bean> cardsToPlay, CollectionIdentifier targetCollection) {
                this.cardsToPlay = cardsToPlay.ToSet();
                this.targetCollection = targetCollection;
            }

            public override void Validate(BuyDecision decision) {
                var beansInHand = decision.AvailableBeans.ToList();
                foreach(var bean in cardsToPlay.ToArray()) {
                    Bean matchingBean;
                    if(beansInHand.TryFirst(b => b.Rank == bean.Rank && b.Suit == bean.Suit, out matchingBean)) {
                        beansInHand.Remove(matchingBean);
                        cardsToPlay.Remove(bean);
                        cardsToPlay.Add(matchingBean);
                    }
                }
                if(cardsToPlay.Count == 0) {
                    throw new RuleViolationException("Must play at least one card.");
                }
                if(cardsToPlay.Except(decision.AvailableBeans).Any()) {
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
                decision.Trader.PlayCardsToCollection(targetCollection, cardsToPlay);
            }

            private BeanCargo GetTargetCollection(BuyDecision decision) {
                return decision.Trader.GetCollection(targetCollection);
            }
        }
    }
}