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

            OnSelected(RevealedCollection, s => {
                PlaySelectedCards(CollectionIdentifier.Revealed);
                return true;
            });

            OnSelected(HiddenCollection, s => {
                PlaySelectedCards(CollectionIdentifier.Hidden);
                return true;
            });

            foreach(var card in CardsInHand) {
                OnSelected(card, s => {
                    var activatedCard = (Card)s;
                    if(selectedCards.Remove(activatedCard)) {
                        return false;
                    } else {
                        selectedCards.Add(activatedCard);
                        return false;
                    }
                });
            }
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
            get { return CardsInHand.Cast<ISprite>().Append(RevealedCollection).Append(HiddenCollection); }
        }
    }
}