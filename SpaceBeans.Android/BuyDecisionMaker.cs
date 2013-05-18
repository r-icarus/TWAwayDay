using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceBeans
{
    internal class BuyDecisionMaker : TraderDecisionMaker<BuyDecision>
    {
        private readonly ICollection<Bean> beansToBuy = new HashSet<Bean>();
        private CollectionIdentifier targetCollection;
        private Suit? selectedSuit;

        public BuyDecisionMaker(Trader trader) : base(trader) { }

        public override void MakeDecision(BuyDecision decision)
        {
            SelectBeansToPlay(decision);
            SelectTargetCollection(decision);
            decision.PlayCards(beansToBuy, targetCollection);
        }

        private void SelectTargetCollection(BuyDecision decision)
        {
           //TODO: Select collection from decision
        }

        private void SelectBeansToPlay(BuyDecision decision)
        {
            //TODO: Select beans to play
        }
       
        private string BuildMenuText(string prompt)
        {
            var menuTextBuilder = new StringBuilder();
            menuTextBuilder.AppendLine("Selected:" + new BeanFormatter().FormatBeans(beansToBuy));
            menuTextBuilder.AppendLine();
            menuTextBuilder.AppendLine(FormatRevealedCollection());
            menuTextBuilder.AppendLine(FormatHiddenCollection());
            menuTextBuilder.AppendLine();
            menuTextBuilder.Append(prompt);
            var menuText = menuTextBuilder.ToString();
            return menuText;
        }
    }
}
