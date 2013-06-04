using System;
using System.Collections.Generic;
using System.Linq;

using CodePhile;
using CodePhile.Games;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public abstract class DecisionModel : IDecisionModel {
        public const int CardWidth = 60;
        public const int CardHeight = 90;
        public const int CardMargin = 5;
        public const int CardOffsetX = CardWidth + CardMargin;
        public const int CardOffsetY = CardHeight + CardMargin;
        public const int CollectionWidth = 66;
        public const int CollectionOffsetX = CollectionWidth + CardMargin;
        private const int LeftMargin = 20;
        private const int TopMargin = 20;

        private readonly List<Card> cardsInHand;
        private readonly List<Card> cardsInRevealed;
        private readonly List<Card> cardsInHidden;

        private readonly RectangleSprite deck;
        private readonly RectangleSprite revealedCollection;
        private readonly RectangleSprite hiddenCollection;
        private readonly RectangleSprite passSprite;
        private readonly ISpaceBeansDecision decision;

        private readonly Texture2D selectableTexture;

        private readonly IDictionary<ISprite, Func<ISprite, bool>> selectedHandlers = new Dictionary<ISprite, Func<ISprite, bool>>();

        private class CollectionSprite : RectangleSprite {
            public CollectionSprite(Rectangle position, Texture2D texture) : base(position, texture) {}

            public override void Draw(SpriteBatch spriteBatch) {
                spriteBatch.Draw(Texture, Position, Color.White * 0.75f);
            }
        }

        protected DecisionModel(ISpaceBeansDecision decision, Textures textures) {
            this.decision = decision;
            var trader = decision.Trader;
            cardsInHand = CreateCardsFromBeans(trader.BeansInHand, textures, index => LeftMargin + (CardOffsetX * (index + 1)), index => TopMargin);
            cardsInRevealed = CreateCardsFromBeans(trader.RevealedCollection, textures, index => LeftMargin + CardOffsetX, index => TopMargin + (CardOffsetY * (index + 2)));
            cardsInHidden = CreateCardsFromBeans(trader.HiddenCollection, textures, index => LeftMargin + CardOffsetX + CollectionOffsetX, index => TopMargin + (CardOffsetY * (index + 2)));
            deck = new RectangleSprite(new Rectangle(LeftMargin, TopMargin, CardWidth, CardHeight), textures.CardBack);
            revealedCollection = new CollectionSprite(new Rectangle(LeftMargin + CardOffsetX, TopMargin + CardOffsetY, CollectionWidth, CardHeight), textures.White);
            hiddenCollection = new CollectionSprite(new Rectangle(LeftMargin + CardOffsetX + CollectionOffsetX, TopMargin + CardOffsetY, CollectionWidth, CardHeight), textures.Black);
            var passableDecision = decision as PassableDecision;
            if(null != passableDecision && passableDecision.CanPass()) {
                passSprite = new RectangleSprite(new Rectangle(LeftMargin, TopMargin + CardOffsetY, CardWidth, 20), textures.Pass);
                OnSelected(passSprite, s => {
                    passableDecision.Pass();
                    return true;
                });
            }
            selectableTexture = textures.White;
        }

        private static List<Card> CreateCardsFromBeans(IEnumerable<Bean> beans, Textures textures, Func<int, int> rectX, Func<int, int> rectY) {
            return beans.Select((bean, index) => new Card(bean, new Rectangle(rectX(index), rectY(index), CardWidth, CardHeight), textures)).ToList();
        }

        public IEnumerable<Card> CardsInHand {
            get { return cardsInHand; }
        }

        public IEnumerable<Card> CardsInRevealed {
            get { return cardsInRevealed; }
        }

        public IEnumerable<Card> CardsInHidden {
            get { return cardsInHidden; }
        }

        protected RectangleSprite Deck {
            get { return deck; }
        }

        protected RectangleSprite RevealedCollection {
            get { return revealedCollection; }
        }

        protected RectangleSprite HiddenCollection {
            get { return hiddenCollection; }
        }

        protected virtual IEnumerable<ISprite> AllSprites {
            get { return CardsInHand.Concat(CardsInRevealed).Concat(CardsInHidden).Cast<ISprite>().Append(revealedCollection).Append(hiddenCollection).Append(deck).Append(passSprite).Where(s => null != s).Cast<ISprite>(); }
        }

        protected void OnSelected(ISprite sprite, Func<ISprite, bool> action) {
            this.selectedHandlers.Add(sprite, action);
        }

        public virtual bool Update(IPointerInput input) {
            if(input.IsNewActivation) {
                var activatedSprite = FindActivatedSprite(input.Location);
                if(null != activatedSprite) {
                    return this.selectedHandlers[activatedSprite](activatedSprite);
                }
            }
            return false;
        }

        public virtual void DrawModel(SpriteBatch spriteBatch) {
            foreach(var sprite in AllSprites) {
                if(this.selectedHandlers.ContainsKey(sprite)) {
                    spriteBatch.Draw(selectableTexture, new Rectangle(sprite.Position.X - 3, sprite.Position.Y - 3, sprite.Position.Width + 6, 1), Color.White * 0.75f);
                    spriteBatch.Draw(selectableTexture, new Rectangle(sprite.Position.X - 3, sprite.Position.Y + sprite.Position.Height + 3, sprite.Position.Width + 6, 1), Color.White * 0.75f);
                    spriteBatch.Draw(selectableTexture, new Rectangle(sprite.Position.X - 3, sprite.Position.Y - 3, 1, sprite.Position.Height + 6), Color.White * 0.75f);
                    spriteBatch.Draw(selectableTexture, new Rectangle(sprite.Position.X + sprite.Position.Width + 3, sprite.Position.Y - 3, 1, sprite.Position.Height + 6), Color.White * 0.75f);
                }
                sprite.Draw(spriteBatch);
            }
        }

        private ISprite FindActivatedSprite(Point location) {
            return AllSprites.Reverse().FirstOrDefault(s => selectedHandlers.ContainsKey(s) && s.Contains(location));
        }
    }
}