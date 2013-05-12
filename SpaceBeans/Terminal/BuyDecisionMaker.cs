using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CodePhile.Terminal;

namespace SpaceBeans {
    internal class BuyDecisionMaker : TraderDecisionMaker<BuyDecision> {
        private readonly ICollection<Bean> beansToBuy = new HashSet<Bean>();
        private CollectionIdentifier targetCollection;
        private Suit? selectedSuit;

        public BuyDecisionMaker(Trader trader) : base(trader) {}

        public override void MakeDecision(BuyDecision decision) {
            SelectBeansToPlay(decision);
            SelectTargetCollection(decision);
            decision.PlayCards(beansToBuy, targetCollection);
        }

        private void SelectTargetCollection(BuyDecision decision) {
            var collectionMenu = new Menu {
                Text = BuildMenuText("Select the collection where the selected cards should be played:"),
                Labels = new [] { NumericLabelSequence.Instance },
            };
            foreach (CollectionIdentifier identifier in Enum.GetValues(typeof(CollectionIdentifier))) {
                collectionMenu.Items.Add(new MenuItem {
                    Text = identifier.ToString(),
                    State = identifier
                });
            }
            targetCollection = collectionMenu.Choose<CollectionIdentifier>();
        }

        private void SelectBeansToPlay(BuyDecision decision) {
            Bean selectedBean;
            do {
                var beanMenu = BuildBeanMenu(decision.AvailableCards);
                selectedBean = beanMenu.Choose<Bean>();
                if(null != selectedBean) {
                    beansToBuy.Add(selectedBean);
                    selectedSuit = selectedBean.Suit;
                }
            } while(selectedBean != null);
        }

        private Menu BuildBeanMenu(IEnumerable<Bean> availableBean) {
            var menuText = BuildMenuText("Choose a card to play:");
            var cardMenu = new Menu {
                Text = menuText,
                Labels = new[] {NumericLabelSequence.Instance},
            };
            var selectableBeans = availableBean
                                    .Except(beansToBuy)
                                    .Where(c => null == selectedSuit || c.Suit == selectedSuit);
            var formatter = new BeanFormatter();
            foreach(var bean in selectableBeans) {
                cardMenu.Items.Add(new MenuItem {
                    State = bean,
                    Text = formatter.FormatBeanLong(bean),
                });
            }
            if(beansToBuy.Count > 0) {
                cardMenu.Items.Add(new MenuItem {
                    Text = "Done"
                });
            }
            return cardMenu;
        }

        private string BuildMenuText(string prompt) {
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