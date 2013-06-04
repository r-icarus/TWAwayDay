using System.Collections.Generic;

namespace SpaceBeans.Xna {
    public class SellDecisionModel : DecisionModel {
        private readonly SellDecision decision;

        public SellDecisionModel(SellDecision decision, Textures textures) : base(decision, textures) {
            this.decision = decision;

			OnSelected(RevealedCollection, s => {
				decision.Sell();
				return true;
			});
        }

        public override bool Update(IPointerInput input) {
            if(!decision.CanSell()) {
                decision.Pass();
                return true;
            }
			return base.Update(input);
        }
    }
}