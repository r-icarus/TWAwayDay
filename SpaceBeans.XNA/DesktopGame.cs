namespace SpaceBeans.Xna {
    public class DesktopGame : Game1 {
        public DesktopGame(SpaceBeansGame game) : base(game) {}

        protected override IPointerInput CreateDefaultPointerInput() {
            return new MouseInput();
        }

        protected override IPointerInput GetCurrentPointerInput(IPointerInput previousPointerInput) {
            return new MouseInput((MouseInput)previousPointerInput);
        }
    }
}
