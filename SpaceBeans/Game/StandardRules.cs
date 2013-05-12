using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceBeans {
    public static class StandardRules {
        public static IEnumerable<Bean> GenerateStandardCards() {
            var suits = (Suit[])Enum.GetValues(typeof(Suit));
            var allCards = (from s in suits
                            from r in Enumerable.Range(1, 3)
                            select new Bean(r, s)).Concat(from s in suits
                                                          from r in Enumerable.Range(4, 6)
                                                          from x in Enumerable.Range(0, 2)
                                                          select new Bean(r, s));
            return allCards;
        }
    }
}
