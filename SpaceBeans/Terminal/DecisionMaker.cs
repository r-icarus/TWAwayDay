using CodePhile.Games;

namespace SpaceBeans {
    internal abstract class DecisionMaker<T> : IDecisionMaker where T : Decision {
        private readonly Trader trader;

        protected DecisionMaker(Trader trader) {
            this.trader = trader;
        }

        bool IDecisionMaker.MakeDecision(Decision decision) {
            var typedDecision = decision as T;
            if(null != typedDecision) {
                MakeDecision(typedDecision);
                return true;
            } else {
                return false;
            }
        }

        public abstract void MakeDecision(T decision);

        protected string FormatHand() {
            return "Hand:    " + new BeanFormatter().FormatBeans(trader.CardsInHand);
        }

        protected string FormatHiddenCollection() {
            return "Hidden:  " + trader.HiddenCollection;
        }

        protected string FormatRevealedCollection() {
            return "Revealed:" + trader.RevealedCollection;
        }
    }
}