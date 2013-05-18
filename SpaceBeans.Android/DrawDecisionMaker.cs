using System.Text;

namespace SpaceBeans
{
    internal class DrawDecisionMaker : TraderDecisionMaker<DrawDecision>
    {

        public DrawDecisionMaker(Trader trader) : base(trader) { }

        public override void MakeDecision(DrawDecision decision)
        {            
            //TODO: UI for YesNoMenus 
            if (!decision.CanPass())
            {
                decision.Draw();
            }
            else
            {
                decision.Pass();
            }
        }
    }
}
