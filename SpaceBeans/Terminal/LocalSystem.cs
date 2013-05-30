using System.Collections.Generic;

namespace SpaceBeans {
    internal class LocalSystem : ConsolePlayer {
        public override void MakeDecision(ISpaceBeansDecision decision) {
            ForEachUntil(GetDecisionMakers(), m => m.MakeDecision(decision));
        }

        private IEnumerable<IDecisionMaker> GetDecisionMakers() {
            yield return new GameSetupDecisionMaker();
        }
    }
}