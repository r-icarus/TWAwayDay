using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace SpaceBeans.Xna {
    public class TouchInput : IPointerInput {

        private readonly bool isActivated;

        public TouchInput() : this(false) {}

        public TouchInput(TouchInput previousInput)
            : this(!previousInput.isActivated) {
        }

        private TouchInput(bool wasActivated) {
            var touchState = TouchPanel.GetState().LastOrDefault();
            if(null != touchState) {
                this.isActivated = touchState.State == TouchLocationState.Pressed;
                this.IsNewActivation = isActivated && wasActivated;
                this.Location = new Point((int)touchState.Position.X, (int)touchState.Position.Y);
            }
        }

        public bool IsNewActivation { get; private set; }
        public Point Location { get; private set; }
    }
}
