using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SpaceBeans {
    public class BeanCargo : IEnumerable<Bean> {

        private readonly ISet<Bean> beans = new HashSet<Bean>();

        public int Count {
            get { return beans.Count; }
        }

        public bool CanAddBean(Bean beanToAdd) {
            return null == Suit || Suit == beanToAdd.Suit;
        }

        private Suit? Suit {
            // TODO: extract FirstOrNullable
            get { return beans.Select(b => (Suit?)b.Suit).FirstOrDefault(); }
        }

        public void AddBeans(IEnumerable<Bean> beansToAdd) {
            foreach(var card in beansToAdd) {
                beans.Add(card);
            }
        }

        public IEnumerator<Bean> GetEnumerator() {
            return beans.GetEnumerator();
        }

        public override string ToString() {
            return new BeanFormatter().FormatBeans(beans);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}