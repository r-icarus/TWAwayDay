using Microsoft.Xna.Framework;

namespace SpaceBeans.Xna {
    public interface IPointerInput {
        bool IsNewActivation { get; }
        Point Location { get; }
    }
}