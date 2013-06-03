using System.Collections.Generic;

namespace SpaceBeans.Xna {
    public class SellDecisionModel : DecisionModel {
        private readonly SellDecision decision;

        public SellDecisionModel(SellDecision decision, Textures textures) : base(decision, textures) {
            this.decision = decision;
        }

        public override bool Update(IPointerInput input) {
            if(base.Update(input)) {
                return true;
            }
            if(!decision.CanSell()) {
                decision.Pass();
                return true;
            }
            if(input.IsNewActivation) {
                var activatedSprite = FindActivatedSprite(input.Location);
                if(RevealedCollection == activatedSprite) {
                    decision.Sell();
                    return true;
                }
            }
            return false;
        }

        protected override IEnumerable<ISprite> SelectableSprites {
            get {
                yield return RevealedCollection;
            }
        }
    }
}