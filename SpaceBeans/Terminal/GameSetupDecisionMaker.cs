using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceBeans {
    internal class GameSetupDecisionMaker : DecisionMaker<SetupDrawPileDecision> {
        public override void MakeDecision(SetupDrawPileDecision decision) {
            var allCards = StandardRules.GenerateStandardCards();
            allCards = Randomize(allCards);
            decision.AddBeans(allCards);
        }

        private static IEnumerable<T> Randomize<T>(IEnumerable<T> source) {
            var sourceCopy = source.ToList();
            var rand = new Random();
            while (sourceCopy.Count > 0) {
                var nextIndex = rand.Next(0, sourceCopy.Count - 1);
                yield return sourceCopy[nextIndex];
                sourceCopy.RemoveAt(nextIndex);
            }
        }
    }
}