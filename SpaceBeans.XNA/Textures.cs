using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private readonly Texture2D cardBack;
        private readonly Texture2D draw;
        private readonly Texture2D pass;
        private readonly Texture2D digit0;
        private readonly Texture2D digit1;
        private readonly Texture2D digit2;
        private readonly Texture2D digit3;
        private readonly Texture2D digit4;
        private readonly Texture2D digit5;
        private readonly Texture2D digit6;
        private readonly Texture2D digit7;
        private readonly Texture2D digit8;
        private readonly Texture2D digit9;

        public Textures(GraphicsDevice graphicsDevice, ContentManager content) {
            black = CreateSolidColorTexture(graphicsDevice, Color.Black);
            blue = CreateSolidColorTexture(graphicsDevice, Color.Blue);
            green = CreateSolidColorTexture(graphicsDevice, Color.Green);
            orange = CreateSolidColorTexture(graphicsDevice, Color.Orange);
            purple = CreateSolidColorTexture(graphicsDevice, Color.Purple);
            red = CreateSolidColorTexture(graphicsDevice, Color.Red);
            silver = CreateSolidColorTexture(graphicsDevice, Color.Silver);
            white = CreateSolidColorTexture(graphicsDevice, Color.White);
            yellow = CreateSolidColorTexture(graphicsDevice, Color.Yellow);
            draw = content.Load<Texture2D>("draw");
            cardBack = content.Load<Texture2D>("card_back");
            pass = content.Load<Texture2D>("pass");
            digit0 = content.Load<Texture2D>("0");
            digit1 = content.Load<Texture2D>("1");
            digit2 = content.Load<Texture2D>("2");
            digit3 = content.Load<Texture2D>("3");
            digit4 = content.Load<Texture2D>("4");
            digit5 = content.Load<Texture2D>("5");
            digit6 = content.Load<Texture2D>("6");
            digit7 = content.Load<Texture2D>("7");
            digit8 = content.Load<Texture2D>("8");
            digit9 = content.Load<Texture2D>("9");
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

        public Texture2D Draw {
            get { return draw; }
        }

        public Texture2D Pass {
            get { return pass; }
        }

        public Texture2D CardBack {
            get { return cardBack; }
        }

        public Texture2D GetDigit(int digit) {
            switch(digit) {
                case 0:
                    return digit0;
                case 1:
                    return digit1;
                case 2:
                    return digit2;
                case 3:
                    return digit3;
                case 4:
                    return digit4;
                case 5:
                    return digit5;
                case 6:
                    return digit6;
                case 7:
                    return digit7;
                case 8:
                    return digit8;
                case 9:
                    return digit9;
                default:
                    throw new ArgumentException("Digit must be between 0 and 9 inclusive.");
            }
        }
    }
}
