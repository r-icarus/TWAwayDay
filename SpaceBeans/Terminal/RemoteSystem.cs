namespace SpaceBeans {
    internal class RemoteSystem : RemoteConsolePlayer {
        public static readonly RemoteSystem Instance = new RemoteSystem();

        protected override string GetName() {
            return "System";
        }
    }
}