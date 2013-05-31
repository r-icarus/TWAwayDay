using System.Collections.Generic;
using System.Linq;

using CodePhile;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public class DrawDecisionModel : DecisionModel {
        private readonly DrawDecision drawDecision;
        private readonly RectangleSprite drawSprite;
        private readonly RectangleSprite passSprite;

        public DrawDecisionModel(DrawDecision drawDecision, Textures textures) : base(drawDecision.Trader, textures) {
            this.drawDecision = drawDecision;
            var drawSpriteRectangle = new Rectangle((CardsInHand.Count() + 1) * CardOffsetX, 20, CardWidth, CardHeight);
            drawSprite = new RectangleSprite(drawSpriteRectangle, textures.White);
            passSprite = new RectangleSprite(new Rectangle(drawSpriteRectangle.X + CardOffsetX, 20, CardWidth, CardHeight), textures.Black);
        }

        public override void DrawModel(SpriteBatch spriteBatch) {
            base.DrawModel(spriteBatch);

            drawSprite.Draw(spriteBatch);
            passSprite.Draw(spriteBatch);
        }

        public override bool Update(IPointerInput input) {
            if(!drawDecision.CanPass()) {
                drawDecision.Draw();
                return true;
            }
            if(input.IsNewActivation) {
                var activatedSprite = FindActivatedSprite(input.Location);
                if(activatedSprite == drawSprite) {
                    drawDecision.Draw();
                    return true;
                } else if(activatedSprite == passSprite) {
                    drawDecision.Pass();
                    return true;
                }
            }
            return false;
        }

        protected override IEnumerable<ISprite> AllSprites {
            get {
                return base.AllSprites.Append(drawSprite).Append(passSprite);
            }
        }
    }
}
