using CodePhile.Games;

namespace SpaceBeans {
    public class DrawPhase : SingleDecisionGamePart {

        private readonly Trader trader;
        private readonly DrawPile drawPile;

        public DrawPhase(Trader trader, DrawPile drawPile) {
            this.trader = trader;
            this.drawPile = drawPile;
        }

        protected override Decision GetDecision() {
            return new DrawDecision(trader, drawPile);
        }
    }
}
