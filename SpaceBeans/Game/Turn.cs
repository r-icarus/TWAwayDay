using System.Collections.Generic;

using CodePhile.Games;

namespace SpaceBeans {
    public class Turn : SequentialParentGamePart {

        private readonly Trader trader;
        private readonly DrawPile drawPile;
        private readonly DiscardPile discardPile;
        private readonly Trader receivingTrader;

        public Turn(Trader trader, DrawPile drawPile, DiscardPile discardPile, Trader receivingTrader) {
            this.trader = trader;
            this.drawPile = drawPile;
            this.discardPile = discardPile;
            this.receivingTrader = receivingTrader;
        }

        public Trader Player {
            get { return trader; }
        }

        protected override IEnumerator<GamePart> GetGameParts() {
            yield return new DrawPhase(trader, drawPile);
            yield return new SellPhase(trader, discardPile);
            yield return new BuyPhase(trader);
        }

        public override void End() {
            receivingTrader.AddBeansToHand(trader.PassHand());
        }
    }
}
