using CodePhile.Games;

namespace SpaceBeans {
    public class SellPhase : PassableGamePart {

        private readonly Trader trader;
        private readonly DiscardPile discardPile;

        public SellPhase(Trader trader, DiscardPile discardPile) {
            this.trader = trader;
            this.discardPile = discardPile;
        }

        protected override PassableDecision GetPassableDecision() {
            return new SellDecision(trader, discardPile);
        }
    }
}
