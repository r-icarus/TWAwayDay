namespace SpaceBeans {
    public class RemotePlayer : RemoteConsolePlayer {
        public RemotePlayer(string connectionId) : base(connectionId) {
        }

        protected override string GetName() {
            return Trader.Name;
        }
    }
}