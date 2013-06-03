using Microsoft.Xna.Framework.Graphics;

using SpaceBeans.Xna;

namespace SpaceBeans.Android {
    public class AndroidGame : MobileGame {
        public AndroidGame(SpaceBeansGame game) : base(game) {
            this.Window.OrientationChanged += (sender, args) =>
                {
                    this.GraphicsDevice.Reset(new PresentationParameters { BackBufferHeight = 240, BackBufferWidth = 320});
                };
        }
    }
}