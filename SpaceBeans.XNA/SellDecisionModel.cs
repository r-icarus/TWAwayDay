namespace SpaceBeans.Xna {
    public class SellDecisionModel : DecisionModel {
        private readonly SellDecision decision;

        public SellDecisionModel(SellDecision decision, Textures textures) : base(decision.Trader, textures) {
            this.decision = decision;
        }

        public override bool Update(IPointerInput input) {
            if(!decision.CanSell()) {
                decision.Pass();
                return true;
            } else if(!decision.CanPass()) {
                decision.Sell();
                return true;
            }
            return false;
        }
    }
}