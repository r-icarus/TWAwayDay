using System;
using System.Collections.Generic;
using System.Linq;
using SpaceBeans.Android.Extensions;

namespace SpaceBeans
{
    public abstract class AndroidPlayer : CodePhile.Games.IDecisionMaker
    {
        private readonly Trader trader;


        protected AndroidPlayer() {}


        protected AndroidPlayer(int position)
        {
            trader = new Trader("Trader " + position);
        }


        public Trader Trader {
            get { return trader; }
        }


        public abstract void MakeDecision(ISpaceBeansDecision decision);


        public static void ForEachUntil<T>(IEnumerable<T> decisionMakers, Func<T, bool> predicate) {
            // TODO: improve this            
            decisionMakers.TakeUntil(predicate).ToArray();
        }
    }
}
