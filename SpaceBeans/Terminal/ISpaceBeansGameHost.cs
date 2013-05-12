using System.ServiceModel;

using CodePhile.Games;

namespace SpaceBeans {
    [ServiceContract(CallbackContract = typeof(ISpaceBeansGameClient))]
    [ServiceKnownType(typeof(SpaceBeansSeat))]
    public interface ISpaceBeansGameHost : IGameHost, ISpaceBeansGameClient { }
}