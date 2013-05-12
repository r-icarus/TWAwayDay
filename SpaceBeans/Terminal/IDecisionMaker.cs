using CodePhile.Games;

namespace SpaceBeans {
    internal interface IDecisionMaker {
        bool MakeDecision(ISpaceBeansDecision decision);
    }
}