using System.Collections.Generic;

namespace SpaceBeans {
    public class LocalPlayer : ConsolePlayer {
        public LocalPlayer(int position) : base(position) {}

        public override void MakeDecision(ISpaceBeansDecision decision) {
            ForEachUntil(GetDecisionMakers(), m => m.MakeDecision(decision));
        }

        private IEnumerable<IDecisionMaker> GetDecisionMakers() {
            yield return new DrawDecisionMaker(Trader);
            yield return new SellDecisionMaker(Trader);
            yield return new BuyDecisionMaker(Trader);
        }
    }
}