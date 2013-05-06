using CodePhile.Games;

namespace SpaceBeans {
    public class BuyPhase : SingleDecisionGamePart {
        private readonly Trader trader;

        public BuyPhase(Trader trader) {
            this.trader = trader;
        }

        protected override Decision GetDecision() {
            return new BuyDecision(trader);
        }
    }
}
