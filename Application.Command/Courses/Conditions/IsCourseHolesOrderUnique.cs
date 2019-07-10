using Domain.Commands;
using Domain.Entities;
using System.Linq;

namespace Application.Command.Courses.Conditions
{
    /// <summary>
    /// Determines if the order of course holes is unique
    /// </summary>
    public class IsCourseHolesOrderUnique : Condition<Course>
    {
        public override bool Validate(Course entity)
        {
            var holeOrders   = entity.Holes.Select(h => h.Order);
            var uniqueOrders = holeOrders.Distinct();
          
            return holeOrders.Count() == uniqueOrders.Count();
        }
    }
}
