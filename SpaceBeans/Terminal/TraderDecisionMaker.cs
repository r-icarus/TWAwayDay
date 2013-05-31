using CodePhile.Games;

namespace SpaceBeans {
    internal abstract class TraderDecisionMaker<T> : DecisionMaker<T> where T : Decision {
        private readonly Trader trader;

        public TraderDecisionMaker(Trader trader) {
            this.trader = trader;
        }

        public string FormatHand() {
            return "Hand:    " + new BeanFormatter().FormatBeans(trader.BeansInHand);
        }

        public string FormatHiddenCollection() {
            return "Hidden:  " + trader.HiddenCollection;
        }

        public string FormatRevealedCollection() {
            return "Revealed:" + trader.RevealedCollection;
        }
    }
}