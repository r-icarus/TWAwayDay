using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public interface IDecisionModel {
        bool Update(IPointerInput input);
        void DrawModel(SpriteBatch spriteBatch);
    }
}
