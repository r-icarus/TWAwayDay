using System.Text;

namespace SpaceBeans
{
    internal class SellDecisionMaker : TraderDecisionMaker<SellDecision>
    {
        public SellDecisionMaker(Trader trader) : base(trader) { }

        public override void MakeDecision(SellDecision decision)
        {
            //TODO: UI for sell
            if (decision.CanSell() && (!decision.CanPass()))
            {
                decision.Sell();
            }
            else
            {
                decision.Pass();
            }
        }
    }
}
