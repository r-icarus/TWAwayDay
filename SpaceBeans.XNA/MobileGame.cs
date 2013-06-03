namespace SpaceBeans.Xna {
    public class MobileGame : Game1 {
        public MobileGame(SpaceBeansGame game) : base(game) {}

        protected override IPointerInput CreateDefaultPointerInput() {
            return new TouchInput();
        }

        protected override IPointerInput GetCurrentPointerInput(IPointerInput previousPointerInput) {
            return new TouchInput((TouchInput)previousPointerInput);
        }
    }
}
