using System.Collections.Generic;
using System.ServiceModel;

using CodePhile.Games;

namespace SpaceBeans {
    [ServiceContract]
    [ServiceKnownType(typeof(DrawDecision.DrawResult))]
    [ServiceKnownType(typeof(BuyDecision.BuyBeansResult))]
    [ServiceKnownType(typeof(HashSet<Bean>))]
    [ServiceKnownType(typeof(SetupDrawPileDecision.AddBeansResult))]
    [ServiceKnownType(typeof(SellDecision.SellResult))]
    public interface ISpaceBeansGameClient : IGameClient { }
}