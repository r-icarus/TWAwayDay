using System;

using CodePhile.Games;

namespace SpaceBeans {
    public class RemotePlayer : ConsolePlayer {
        private readonly ISpaceBeansGameClient callback;

        public RemotePlayer(int position, ISpaceBeansGameClient callback) : base(position) {
            this.callback = callback;
        }

        public void ResolveDecision(DecisionResult decision) {
            callback.ResolveDecision(decision);
        }

        public override void MakeDecision(ISpaceBeansDecision decision) {
            Console.Clear();
            Console.WriteLine("Waiting for {0}...", Trader.Name);
            ((Decision)decision).WaitUntilComplete();
        }
    }
}