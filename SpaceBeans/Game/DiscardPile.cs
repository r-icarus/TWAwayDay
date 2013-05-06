using System.Collections.Generic;

namespace SpaceBeans {
    public class DiscardPile {

        private readonly List<Bean> beans = new List<Bean>();

        public int Count {
            get { return beans.Count; }
        }

        public void DiscardBeans(IEnumerable<Bean> discardedBeans) {
            beans.AddRange(discardedBeans);
        }

        public IEnumerable<Bean> Reshuffle() {
            // TODO: randomize this
            var result = beans.ToArray();
            beans.Clear();
            return result;
        }
    }
}