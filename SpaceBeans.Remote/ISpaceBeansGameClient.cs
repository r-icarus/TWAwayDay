using System.Collections.Generic;
using System.ServiceModel;

using CodePhile.Games;

namespace SpaceBeans {
    [ServiceKnownType(typeof(DrawDecision.DrawResult))]
    [ServiceKnownType(typeof(BuyDecision.BuyBeansResult))]
    [ServiceKnownType(typeof(HashSet<Bean>))]
    [ServiceKnownType(typeof(SetupDrawPileDecision.AddBeansResult))]
    [ServiceKnownType(typeof(SellDecision.SellResult))]
    public interface ISpaceBeansGameClient : IGameClient {
        [OperationContract(IsOneWay = true)]
        void PlayerJoined(SpaceBeansSeat seat, string name);

        [OperationContract(IsOneWay = true)]
        void GameStarted();
    }
}