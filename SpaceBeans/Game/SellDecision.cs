using System;
using System.Runtime.Serialization;

using CodePhile.Games;

namespace SpaceBeans {
    public class SellDecision : PassableDecision, ISpaceBeansDecision {

        private readonly Trader trader;
        private readonly DiscardPile discardPile;

        public SellDecision(Trader trader, DiscardPile discardPile) {
            this.trader = trader;
            this.discardPile = discardPile;
        }

        protected override Exception GetPassError() {
            if (!trader.HasLegalPlay()) {
                return new RuleViolationException("Must convert revealed collection if there is no legal play.");
            }
            return null;
        }

        public bool CanSell() {
            return trader.RevealedCollection.Count > 0;
        }

        public void Sell() {
            OnDecided(new SellResult());
        }

        [DataContract]
        public class SellResult : DecisionResult<SellDecision> {
            public override void Validate(SellDecision decision) {
                if(!decision.CanSell()) {
                    throw new RuleViolationException("Cannot convert empty collection.");
                }
            }

            public override void Resolve(SellDecision decision) {
                var discardedCards = decision.trader.SellRevealedCollection();
                decision.discardPile.DiscardBeans(discardedCards);
            }
        }

        public Trader Trader { get { return trader; } }
    }
}