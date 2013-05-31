using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public abstract class DecisionModel : IDecisionModel {
        public const int CardWidth = 42;
        public const int CardHeight = 84;
        public const int CardMargin = 8;
        public const int CardOffsetX = CardWidth + CardMargin;
        public const int CardOffsetY = CardHeight + CardMargin;

        private readonly List<Card> cardsInHand;
        private readonly List<Card> cardsInRevealed;
        private readonly List<Card> cardsInHidden;

        protected DecisionModel(Trader trader, Textures textures)
            : this(trader, textures, (b, r, t) => new Card(b, r, t)) {
        }

        protected DecisionModel(Trader trader, Textures textures, Func<Bean, Rectangle, Textures, Card> cardFactory) {
            cardsInHand = CreateCardsFromBeans(trader.BeansInHand, textures, index => 20 + (CardOffsetX * index), index => 20, cardFactory);
            cardsInRevealed = CreateCardsFromBeans(trader.RevealedCollection, textures, index => 20, index => 20 + (CardOffsetY * (index + 1)), cardFactory);
            cardsInHidden = CreateCardsFromBeans(trader.HiddenCollection, textures, index => 20 + CardOffsetX, index => 20 + (CardOffsetY * (index + 1)), cardFactory);
        }

        private static List<Card> CreateCardsFromBeans(IEnumerable<Bean> beans, Textures textures, Func<int, int> rectX, Func<int, int> rectY, Func<Bean, Rectangle, Textures, Card> cardFactory) {
            return beans.Select((bean, index) => cardFactory(bean, new Rectangle(rectX(index), rectY(index), CardWidth, CardHeight), textures)).ToList();
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

        protected virtual IEnumerable<ISprite> AllSprites {
            get { return CardsInHand.Concat(CardsInRevealed).Concat(CardsInHidden); }
        }

        public abstract bool Update(IPointerInput input);

        public virtual void DrawModel(SpriteBatch spriteBatch) {
            foreach(var sprite in AllSprites) {
                sprite.Draw(spriteBatch);
            }
        }

        protected ISprite FindActivatedSprite(Point location) {
            return AllSprites.FirstOrDefault(s => s.Contains(location));
        }
    }
}