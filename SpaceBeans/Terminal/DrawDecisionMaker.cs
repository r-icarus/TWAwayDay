using System.Text;

namespace SpaceBeans {
    internal class DrawDecisionMaker : TraderDecisionMaker<DrawDecision> {

        public DrawDecisionMaker(Trader trader) : base(trader) {}

        public override void MakeDecision(DrawDecision decision) {
            var menuText = new StringBuilder();
            menuText.AppendLine(FormatHand());
            menuText.AppendLine();
            menuText.AppendLine(FormatRevealedCollection());
            menuText.AppendLine(FormatHiddenCollection());
            menuText.AppendLine();
            menuText.Append("Draw 2 cards?");
            if (!decision.CanPass() || YesNoMenu.Ask(menuText.ToString())) {
                decision.Draw();
            } else {
                decision.Pass();
            }
        }
    }
}