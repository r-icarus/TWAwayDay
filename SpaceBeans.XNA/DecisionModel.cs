using System;
using System.Collections.Generic;
using System.Linq;

using CodePhile;
using CodePhile.Games;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public abstract class DecisionModel : IDecisionModel {
        public const int CardWidth = 34;
        public const int CardHeight = 50;
        public const int CardMargin = 8;
        public const int CardOffsetX = CardWidth + CardMargin;
        public const int CardOffsetY = CardHeight + CardMargin;
        public const int CollectionWidth = 40;
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

        private class CollectionSprite : RectangleSprite {
            public CollectionSprite(Rectangle position, Texture2D texture) : base(position, texture) {}

            public override void Draw(SpriteBatch spriteBatch) {
                spriteBatch.Draw(Texture, Position, Color.White * 0.75f);
            }
        }

        protected DecisionModel(ISpaceBeansDecision decision, Textures textures) {
            this.decision = decision;
            var trader = decision.Trader;
            cardsInHand = CreateCardsFromBeans(trader.BeansInHand, textures, index => LeftMargin + (CardOffsetX * index), index => TopMargin);
            cardsInRevealed = CreateCardsFromBeans(trader.RevealedCollection, textures, index => LeftMargin, index => TopMargin + (CardOffsetY * (index + 2)));
            cardsInHidden = CreateCardsFromBeans(trader.HiddenCollection, textures, index => LeftMargin + CardOffsetX, index => TopMargin + (CardOffsetY * (index + 2)));
            deck = new RectangleSprite(new Rectangle(LeftMargin + (CollectionOffsetX * 2), TopMargin + CardOffsetY, CardWidth, CardHeight), textures.CardBack);
            revealedCollection = new CollectionSprite(new Rectangle(LeftMargin, TopMargin + CardOffsetY, CollectionWidth, CardHeight), textures.White);
            hiddenCollection = new CollectionSprite(new Rectangle(LeftMargin + CollectionOffsetX, TopMargin + CardOffsetY, CollectionWidth, CardHeight), textures.Black);
            var passableDecision = decision as PassableDecision;
            if(null != passableDecision && passableDecision.CanPass()) {
                passSprite = new RectangleSprite(new Rectangle(250, 500, CardWidth, 20), textures.Pass);
            }
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
            get { return CardsInHand.Concat(CardsInRevealed).Concat(CardsInHidden).Append(revealedCollection).Append(hiddenCollection).Append(deck).Append(passSprite).Where(s => null != s).Cast<ISprite>(); }
        }

        public virtual bool Update(IPointerInput input) {
            if(input.IsNewActivation && passSprite == FindActivatedSprite(input.Location)) {
                ((PassableDecision)decision).Pass();
                return true;
            }
            return false;
        }

        public virtual void DrawModel(SpriteBatch spriteBatch) {
            foreach(var sprite in AllSprites) {
                sprite.Draw(spriteBatch);
            }
        }

        protected ISprite FindActivatedSprite(Point location) {
            return SelectableSprites.Append(passSprite).Where(s => null != s).Reverse().FirstOrDefault(s => s.Contains(location));
        }

        protected abstract IEnumerable<ISprite> SelectableSprites { get; }
    }
}