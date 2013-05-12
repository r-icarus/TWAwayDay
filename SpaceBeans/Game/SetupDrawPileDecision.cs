using System.Collections.Generic;
using System.Runtime.Serialization;

using CodePhile;
using CodePhile.Games;

namespace SpaceBeans {
    public class SetupDrawPileDecision : Decision, ISpaceBeansDecision {

        private readonly DrawPile drawPile;

        public SetupDrawPileDecision(DrawPile drawPile) {
            this.drawPile = drawPile;
        }

        public Trader Trader {
            get { return null; }
        }

        public void AddBeans(IEnumerable<Bean> beans) {
            OnDecided(new AddBeansResult(beans));
        }

        [DataContract]
        public class AddBeansResult : DecisionResult<SetupDrawPileDecision> {
            [DataMember(Name = "Beans")] 
            private readonly HashSet<Bean> beans;

            public AddBeansResult(IEnumerable<Bean> beans) {
                this.beans = beans.ToSet();
            }

            public override void Validate(SetupDrawPileDecision decision) {
                // TODO: validate?
            }

            public override void Resolve(SetupDrawPileDecision decision) {
                decision.drawPile.AddBeans(beans);
            }
        }
    }
}