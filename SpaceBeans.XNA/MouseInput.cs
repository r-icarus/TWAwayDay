using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceBeans.Xna {
    public class MouseInput : IPointerInput {

        private readonly bool isActivated;

        public MouseInput() : this(false) {}

        public MouseInput(MouseInput previousInput)
            : this(!previousInput.isActivated) {
        }

        private MouseInput(bool wasActivated) {
            this.isActivated = Mouse.GetState().LeftButton == ButtonState.Pressed;
            this.IsNewActivation = isActivated && wasActivated;
            this.Location = new Point(Mouse.GetState().X, Mouse.GetState().Y);
        }

        public bool IsNewActivation { get; private set; }
        public Point Location { get; private set; }
    }
}