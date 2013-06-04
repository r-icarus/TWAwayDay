using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public interface ISprite {
        void Draw(SpriteBatch spriteBatch);

        bool Contains(Point point);

        Rectangle Position { get; }
    }
}