using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public class BuyDecisionModel : DecisionModel {
        private readonly BuyDecision decision;
        private readonly ISet<Card> selectedCards = new HashSet<Card>();
        private readonly Texture2D haloTexture;

        public BuyDecisionModel(BuyDecision decision, Textures textures) : base(decision.Trader, textures) {
            this.decision = decision;
            haloTexture = textures.White;
        }

        public override bool Update(IPointerInput input) {
            if(input.IsNewActivation) {
                var activatedSprite = FindActivatedSprite(input.Location);
                var activatedCard = activatedSprite as Card;
                if(null != activatedCard) {
                    if(selectedCards.Remove(activatedCard)) {
                        return false;
                    } else if (decision.AvailableBeans.Contains(activatedCard.Bean)) {
                        selectedCards.Add(activatedCard);
                        return false;
                    }
                }
            }
            return false;
        }

        public override void DrawModel(SpriteBatch spriteBatch) {
            foreach (var selectedCard in selectedCards) {
                var halo = new Rectangle(selectedCard.Position.X, selectedCard.Position.Y, selectedCard.Position.Width, selectedCard.Position.Height);
                halo.Inflate(2, 2);
                spriteBatch.Draw(haloTexture, halo, Color.White);
            }

            base.DrawModel(spriteBatch);
        }
    }
}