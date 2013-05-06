using System.Collections.Generic;

namespace SpaceBeans {
    public class DrawPile {
        private readonly DiscardPile discardPile;
        private readonly List<Bean> beans = new List<Bean>();

        public DrawPile(DiscardPile discardPile) {
            this.discardPile = discardPile;
        }

        public IEnumerable<Bean> Draw(int count) {
            if(beans.Count < count) {
                beans.AddRange(discardPile.Reshuffle());
            }
            var result = beans.GetRange(0, count);
            beans.RemoveRange(0, count);
            return result;
        }

        public void AddBeans(IEnumerable<Bean> beansToAdd) {
            beans.AddRange(beansToAdd);
        }
    }
}