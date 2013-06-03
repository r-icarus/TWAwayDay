using System.Collections.Generic;
using System.Linq;

using CodePhile;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public class DrawDecisionModel : DecisionModel {
        private readonly DrawDecision drawDecision;
        private readonly RectangleSprite drawSprite;

        public DrawDecisionModel(DrawDecision drawDecision, Textures textures) : base(drawDecision, textures) {
            this.drawDecision = drawDecision;
            //var drawSpriteRectangle = new Rectangle((CardsInHand.Count() + 1) * CardOffsetX, 20, CardWidth, CardHeight);
            var drawSpriteRectangle = new Rectangle(Deck.Position.X, Deck.Position.Y + ((Deck.Position.Height - 20) / 2), CardWidth, 20);
            drawSprite = new RectangleSprite(drawSpriteRectangle, textures.Draw);
        }

        protected override IEnumerable<ISprite> SelectableSprites {
            get {
                yield return drawSprite;
            }
        }

        public override bool Update(IPointerInput input) {
            //if(!drawDecision.CanPass()) {
            //    drawDecision.Draw();
            //    return true;
            //}
            if(base.Update(input)) {
                return true;
            }
            if(input.IsNewActivation) {
                var activatedSprite = FindActivatedSprite(input.Location);
                if(activatedSprite == drawSprite) {
                    drawDecision.Draw();
                    return true;
                }
            }
            return false;
        }

        protected override IEnumerable<ISprite> AllSprites {
            get {
                return base.AllSprites.Append(drawSprite);
            }
        }
    }
}
