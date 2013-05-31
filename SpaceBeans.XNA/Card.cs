using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public class Card : RectangleSprite {
        public const int DotWidth = 6;
        public const int DotHeight = 6;

        private readonly Bean bean;
        private readonly Texture2D dot;

        public Card(Bean bean, Rectangle position, Textures textures) 
            : base(position, GetCardColorFromBeanSuit(bean, textures)) {
            this.bean = bean;
            this.dot = textures.Black;
        }

        public Bean Bean {
            get { return bean; }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            DrawDots(dot, spriteBatch);
        }

        private void DrawDots(Texture2D dot, SpriteBatch spriteBatch) {
            var leftColumn = Position.Width / 4;
            var centerColumn = Position.Width / 2;
            var rightColumn = Position.Width / 4 * 3;
            var topRow = Position.Height / 5;
            var topCenterRow = Position.Height * 7 / 20;
            var centerRow = Position.Height / 2;
            var bottomCenterRow = Position.Height * 13 / 20;
            var bottomRow = Position.Height * 4 / 5;
            if(new[] {1, 3, 5, 9}.Contains(bean.Rank)) {
                DrawDot(dot, centerColumn, centerRow, spriteBatch);
            }
            if(new[] {2, 3}.Contains(bean.Rank)) {
                DrawDot(dot, centerColumn, topRow, spriteBatch);
                DrawDot(dot, centerColumn, bottomRow, spriteBatch);
            }
            if(bean.Rank >= 4) {
                DrawDot(dot, leftColumn, topRow, spriteBatch);
                DrawDot(dot, leftColumn, bottomRow, spriteBatch);
                DrawDot(dot, rightColumn, topRow, spriteBatch);
                DrawDot(dot, rightColumn, bottomRow, spriteBatch);
            }
            if(new[] {6, 7, 8}.Contains(bean.Rank)) {
                DrawDot(dot, leftColumn, centerRow, spriteBatch);
                DrawDot(dot, rightColumn, centerRow, spriteBatch);
            }
            if(new[] {7, 8}.Contains(bean.Rank)) {
                DrawDot(dot, centerColumn, bottomCenterRow, spriteBatch);
            }
            if(8 == bean.Rank) {
                DrawDot(dot, centerColumn, topCenterRow, spriteBatch);
            }
            if(9 == bean.Rank) {
                DrawDot(dot, leftColumn, topCenterRow, spriteBatch);
                DrawDot(dot, leftColumn, bottomCenterRow, spriteBatch);
                DrawDot(dot, rightColumn, topCenterRow, spriteBatch);
                DrawDot(dot, rightColumn, bottomCenterRow, spriteBatch);
            }
        }

        private void DrawDot(Texture2D dot, int dotOffsetX, int dotOffsetY, SpriteBatch spriteBatch) {
            var dotRect = new Rectangle(Position.X + dotOffsetX - (DotWidth / 2), Position.Y + dotOffsetY - (DotHeight / 2), DotWidth, DotHeight);
            spriteBatch.Draw(dot, dotRect, Color.White);
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
