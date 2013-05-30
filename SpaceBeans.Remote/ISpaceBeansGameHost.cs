using System.Collections.Generic;
using System.ServiceModel;

using CodePhile.Games;

namespace SpaceBeans {
    [ServiceContract(CallbackContract = typeof(ISpaceBeansGameClient), SessionMode = SessionMode.Required)]
    [ServiceKnownType(typeof(SpaceBeansSeat))]
    [ServiceKnownType(typeof(DrawDecision.DrawResult))]
    [ServiceKnownType(typeof(BuyDecision.BuyBeansResult))]
    [ServiceKnownType(typeof(HashSet<Bean>))]
    [ServiceKnownType(typeof(SetupDrawPileDecision.AddBeansResult))]
    [ServiceKnownType(typeof(SellDecision.SellResult))]
    public interface ISpaceBeansGameHost : IGameHost { }
}