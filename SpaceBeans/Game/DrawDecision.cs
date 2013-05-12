using System;
using System.Runtime.Serialization;

using CodePhile.Games;

namespace SpaceBeans {
    public class DrawDecision : PassableDecision, ISpaceBeansDecision {

        private readonly Trader trader;
        private readonly DrawPile drawPile;

        public DrawDecision(Trader trader, DrawPile drawPile) {
            this.trader = trader;
            this.drawPile = drawPile;
        }

        protected override Exception GetPassError() {
            if(trader.HandCount == 0) {
                return new RuleViolationException("Must draw cards if hand is empty.");
            }
            return null;
        }

        public void Draw() {
            OnDecided(new DrawResult());
        }

        public Trader Trader {
            get { return trader; }
        }

        [DataContract]
        public class DrawResult : DecisionResult<DrawDecision> {
            public override void Validate(DrawDecision decision) {
            }

            public override void Resolve(DrawDecision decision) {
                decision.trader.AddBeansToHand(decision.drawPile.Draw(2));
            }
        }
    }
}