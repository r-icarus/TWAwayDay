namespace SpaceBeans.Xna {
    public class DrawDecisionModel : DecisionModel {
        public DrawDecisionModel(DrawDecision drawDecision, Textures textures) : base(drawDecision, textures) {
            OnSelected(Deck, s => {
                drawDecision.Draw();
                return true;
            });
        }
    }
}
