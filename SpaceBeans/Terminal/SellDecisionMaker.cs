using System.Text;

namespace SpaceBeans {
    internal class SellDecisionMaker : TraderDecisionMaker<SellDecision> {
        public SellDecisionMaker(Trader trader) : base(trader) {}

        public override void MakeDecision(SellDecision decision) {
            var menuText = new StringBuilder();
            menuText.AppendLine(FormatHand());
            menuText.AppendLine();
            menuText.AppendLine(FormatRevealedCollection());
            menuText.AppendLine(FormatHiddenCollection());
            menuText.AppendLine();
            menuText.Append("Convert revealed collection?");
            if (decision.CanSell() && (!decision.CanPass() || YesNoMenu.Ask(menuText.ToString()))) {
                decision.Sell();
            } else {
                decision.Pass();
            }
        }
    }
}