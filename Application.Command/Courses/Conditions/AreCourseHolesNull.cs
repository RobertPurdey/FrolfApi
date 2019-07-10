using Domain.Commands;
using Domain.Entities;

namespace Application.Command.Courses.Conditions
{
    /// <summary>
    /// Determines course has no holes set
    /// </summary>
    public class AreCourseHolesNull : Condition<Course>
    {
        public override bool Validate(Course entity)
        {
            return entity.Holes == null;
        }
    }
}
