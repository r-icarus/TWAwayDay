using System;

using CodePhile.Games;

namespace SpaceBeans {
    public class SellDecision : PassableDecision {

        private readonly Trader player;
        private readonly DiscardPile discardPile;

        public SellDecision(Trader player, DiscardPile discardPile) {
            this.player = player;
            this.discardPile = discardPile;
        }

        protected override Exception GetPassError() {
            if(!player.HasLegalPlay()) {
                return new RuleViolationException("Must convert revealed collection if there is no legal play.");
            }
            return null;
        }

        public bool CanSell() {
            return player.RevealedCollection.Count > 0;
        }

        public void Sell() {
            OnCompleted(new SellDecisionResult());
        }

        private class SellDecisionResult : DecisionResult<SellDecision> {
            public override void Validate(SellDecision decision) {
                if(!decision.CanSell()) {
                    throw new RuleViolationException("Cannot convert empty collection.");
                }
            }

            public override void Resolve(SellDecision decision) {
                var discardedCards = decision.player.SellRevealedCollection();
                decision.discardPile.DiscardBeans(discardedCards);
            }
        }
    }
}