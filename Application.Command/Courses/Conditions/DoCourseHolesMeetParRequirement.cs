using Domain.Commands;
using Domain.Entities;
using System.Linq;

namespace Application.Command.Courses.Conditions
{
    /// <summary>
    /// Determines if all course holes have a par above 0.
    /// </summary>
    public class DoCourseHolesMeetParRequirement : Condition<Course>
    {
        public override bool Validate(Course entity)
        {
            return entity.Holes.All(h => h.Par > 0);
        }
    }
}
