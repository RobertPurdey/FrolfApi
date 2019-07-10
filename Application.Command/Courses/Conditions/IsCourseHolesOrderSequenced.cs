using Domain.Commands;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Command.Courses.Conditions
{
    /// <summary>
    /// Determines if the course holes order is sequenced from 1 onward.
    /// </summary>
    public class IsCourseHolesOrderSequenced : Condition<Course>
    {
        public override bool Validate(Course entity)
        {
            var uniqueTees   = entity.Holes.Distinct().OrderBy(e => e.Order).Select(e => e.Order);
            var expectedTees = new HashSet<int>();

            var expectedTee = 1;
            foreach (var order in uniqueTees)
            {
                expectedTees.Add(expectedTee++);
            }

            var startsAtOne = uniqueTees.First() == 1;
            var isSequenced = uniqueTees.SequenceEqual(expectedTees.AsEnumerable());

            return startsAtOne && isSequenced;
        }
    }
}
