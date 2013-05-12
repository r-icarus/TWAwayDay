using CodePhile.Games;

namespace SpaceBeans {
    internal class GameSetupPart : SingleDecisionGamePart {

        private readonly DrawPile drawPile;

        public GameSetupPart(DrawPile drawPile) {
            this.drawPile = drawPile;
        }

        protected override Decision GetDecision() {
            return new SetupDrawPileDecision(drawPile);
        }
    }
}