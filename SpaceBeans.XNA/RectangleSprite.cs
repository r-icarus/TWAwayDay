using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public class RectangleSprite : ISprite {
        private readonly Rectangle position;
        private readonly Texture2D texture;

        public RectangleSprite(Rectangle position, Texture2D texture) {
            this.position = position;
            this.texture = texture;
        }

        public Rectangle Position {
            get { return position; }
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public bool Contains(Point point) {
            return position.Contains(point);
        }
    }
}