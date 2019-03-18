using Frolf.Api.Models.Games;

namespace Frolf.Api.ModelDataControllers.Contracts
{
    public interface IGameModelDataController
        : IModelDataController<GameModel, GameFilterModel>
    {

    }
}