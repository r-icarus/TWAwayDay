using System;

using CodePhile.Games;

namespace SpaceBeans {
    public abstract class RemoteConsolePlayer : ConsolePlayer {
        protected RemoteConsolePlayer() { }
        protected RemoteConsolePlayer(string connectionId) : base(connectionId) {}

        public override void MakeDecision(ISpaceBeansDecision decision) {
            Console.Clear();
            Console.WriteLine("Waiting for {0}...", GetName());
            ((Decision)decision).WaitUntilComplete();
        }

        protected abstract string GetName();
    }
}