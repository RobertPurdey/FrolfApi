using Domain.Commands;
using Domain.Entities;

namespace Application.Command.FrolfGroups.Conditions
{
    /// <summary>
    /// Determines if the group is null
    /// </summary>
    public class IsFrolfGroupNull : Condition<FrolfGroup>
    {
        public IsFrolfGroupNull()
        {
        }

        public override bool Validate(FrolfGroup entity)
        {
            return entity == null;
        }
    }
}
