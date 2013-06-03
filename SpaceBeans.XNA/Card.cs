using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public class Card : RectangleSprite {
        public const int DotWidth = 6;
        public const int DotHeight = 6;

        private readonly Bean bean;
        private readonly Texture2D digit;

        public Card(Bean bean, Rectangle position, Textures textures) 
            : base(position, GetCardColorFromBeanSuit(bean, textures)) {
            this.bean = bean;
            this.digit = textures.GetDigit(bean.Rank);
        }

        public Bean Bean {
            get { return bean; }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);

            spriteBatch.Draw(digit, new Rectangle(Position.X, Position.Y, 10, 14), Color.White);
        }

        private static Texture2D GetCardColorFromBeanSuit(Bean bean, Textures textures) {
            switch(bean.Suit) {
                case Suit.Blue:
                    return textures.Blue;
                case Suit.Green:
                    return textures.Green;
                case Suit.Orange:
                    return textures.Orange;
                case Suit.Purple:
                    return textures.Purple;
                case Suit.Red:
                    return textures.Red;
                case Suit.Silver:
                    return textures.Silver;
                case Suit.Yellow:
                    return textures.Yellow;
                default:
                    return textures.Black;
            }
        }
    }
}
