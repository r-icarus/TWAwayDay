using CodePhile.Games;

namespace SpaceBeans
{
    internal abstract class DecisionMaker<T> : IDecisionMaker where T : Decision
    {
        bool IDecisionMaker.MakeDecision(ISpaceBeansDecision decision)
        {
            var typedDecision = decision as T;
            if (null != typedDecision)
            {
                MakeDecision(typedDecision);
                return true;
            }
            else
            {
                return false;
            }
        }

        public abstract void MakeDecision(T decision);
    }
}
