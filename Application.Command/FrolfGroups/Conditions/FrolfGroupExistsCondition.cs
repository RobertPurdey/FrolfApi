using Domain.Commands;
using Domain.Entities;

namespace Application.Command.FrolfGroups.Conditions
{
    /// <summary>
    /// Determines if the group exists
    /// </summary>
    public class FrolfGroupExistsCondition : Condition<FrolfGroup>
    {
        public FrolfGroupExistsCondition()
        {
        }

        public override bool Validate(FrolfGroup entity)
        {
            return entity != null;
        }
    }
}
