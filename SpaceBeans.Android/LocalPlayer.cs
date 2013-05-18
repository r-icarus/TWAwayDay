using System.Collections.Generic;
using SpaceBeans.Android;
using CodePhile.Games;

namespace SpaceBeans
{
    public class LocalPlayer : AndroidPlayer
    {
        public LocalPlayer(int position) : base(position) { }

        public override void MakeDecision(ISpaceBeansDecision decision)
        {
            ForEachUntil(GetDecisionMakers(), m => m.MakeDecision(decision));
        }

        private IEnumerable<IDecisionMaker> GetDecisionMakers()
        {
            yield return new DrawDecisionMaker(Trader);
            yield return new SellDecisionMaker(Trader);
            yield return new BuyDecisionMaker(Trader);
        }
    }
}
