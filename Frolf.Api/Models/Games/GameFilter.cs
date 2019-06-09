using Domain.Entities.Entities.Games;
using Frolf.Api.Models.Contracts;

namespace Frolf.Api.Models.Games
{
    public class GameFilter : IFilterModel
    {
        public GameState? State { get; set; }
    }
}