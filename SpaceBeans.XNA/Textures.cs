using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBeans.Xna {
    public class Textures {
        private readonly Texture2D black;
        private readonly Texture2D blue;
        private readonly Texture2D green;
        private readonly Texture2D orange;
        private readonly Texture2D purple;
        private readonly Texture2D red;
        private readonly Texture2D silver;
        private readonly Texture2D white;
        private readonly Texture2D yellow;

        public Textures(GraphicsDevice graphicsDevice) {
            black = CreateSolidColorTexture(graphicsDevice, Color.Black);
            blue = CreateSolidColorTexture(graphicsDevice, Color.Blue);
            green = CreateSolidColorTexture(graphicsDevice, Color.Green);
            orange = CreateSolidColorTexture(graphicsDevice, Color.Orange);
            purple = CreateSolidColorTexture(graphicsDevice, Color.Purple);
            red = CreateSolidColorTexture(graphicsDevice, Color.Red);
            silver = CreateSolidColorTexture(graphicsDevice, Color.Silver);
            white = CreateSolidColorTexture(graphicsDevice, Color.White);
            yellow = CreateSolidColorTexture(graphicsDevice, Color.Yellow);
        }

        private static Texture2D CreateSolidColorTexture(GraphicsDevice graphicsDevice, Color color) {
            var texture2D = new Texture2D(graphicsDevice, 1, 1);
            texture2D.SetData(new[] {color});
            return texture2D;
        }

        public Texture2D Black {
            get { return black; }
        }

        public Texture2D Blue {
            get { return blue; }
        }

        public Texture2D Green {
            get { return green; }
        }

        public Texture2D Orange {
            get { return orange; }
        }

        public Texture2D Purple {
            get { return purple; }
        }

        public Texture2D Red {
            get { return red; }
        }

        public Texture2D Silver {
            get { return silver; }
        }

        public Texture2D Yellow {
            get { return yellow; }
        }

        public Texture2D White {
            get { return white; }
        }
    }
}
