using System.Collections.Generic;
using System.Linq;

namespace SpaceBeans {
    public class BeanFormatter {
        public string FormatBeans(IEnumerable<Bean> beans) {
            return string.Join(" ", beans.Select(FormatBeanShort));
        }

        public string FormatBeanShort(Bean bean) {
            return bean.Rank.ToString() + bean.Suit.ToString()[0];
        }

        public string FormatBeanLong(Bean bean) {
            return bean.Rank + " of " + bean.Suit;
        }
    }
}