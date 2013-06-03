using System.Collections.Generic;
using System.Linq;

using CodePhile;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public class BuyDecisionModel : DecisionModel {
        private readonly BuyDecision decision;
        private readonly ISet<Card> selectedCards = new HashSet<Card>();
        private readonly Texture2D haloTexture;

        public BuyDecisionModel(BuyDecision decision, Textures textures) : base(decision, textures) {
            this.decision = decision;
            haloTexture = textures.White;
        }

        public override bool Update(IPointerInput input) {
            if(input.IsNewActivation) {
                var activatedSprite = FindActivatedSprite(input.Location);
                if(null == activatedSprite) {
                    return false;
                }
                if(RevealedCollection == activatedSprite) {
                    PlaySelectedCards(CollectionIdentifier.Revealed);
                    return true;
                } else if(HiddenCollection == activatedSprite) {
                    PlaySelectedCards(CollectionIdentifier.Hidden);
                    return true;
                } else {
                    var activatedCard = (Card)activatedSprite;
                    if(selectedCards.Remove(activatedCard)) {
                        return false;
                    } else if(decision.AvailableBeans.Contains(activatedCard.Bean)) {
                        selectedCards.Add(activatedCard);
                        return false;
                    }
                }
            }
            return false;
        }

        private void PlaySelectedCards(CollectionIdentifier collection) {
            this.decision.PlayCards(selectedCards.Select(c => c.Bean), collection);
        }

        public override void DrawModel(SpriteBatch spriteBatch) {
            foreach (var selectedCard in selectedCards) {
                var halo = new Rectangle(selectedCard.Position.X, selectedCard.Position.Y, selectedCard.Position.Width, selectedCard.Position.Height);
                halo.Inflate(2, 2);
                spriteBatch.Draw(haloTexture, halo, Color.White);
            }

            base.DrawModel(spriteBatch);
        }

        protected override IEnumerable<ISprite> SelectableSprites {
            get { return CardsInHand.Append(RevealedCollection).Append(HiddenCollection); }
        }
    }
}