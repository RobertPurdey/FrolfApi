using Domain.Commands;
using Domain.Entities;
using Domain.Query.Contracts;
using System.Linq;

namespace Application.Command.FrolfGroups.Conditions
{

    // todo: remove if not needed

    /// <summary>
    /// Determines if the group exists
    /// </summary>
    public class FrolfGroupExistsCondition : Condition<FrolfGroup>
    {
        private readonly IQueryService<FrolfGroup> frolfGroupService;

        public FrolfGroupExistsCondition(IQueryService<FrolfGroup> frolfGroupQueryService)
        {
            frolfGroupService = frolfGroupQueryService;
        }

        public override bool Validate(FrolfGroup entity)
        {
            return frolfGroupService
                .GetAll()
                .Any(g => g.EntityKey == entity.EntityKey);
        }
    }
}
