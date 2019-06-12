using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Application.Command.Courses.Commands
{
    public class AddCourseCommand : ICommand
    {
        public Course newEntity { get; set; }
    }

    public class AddCourseCommandValidation : CommandPreHandler<AddCourseCommand>
    {
        private readonly IQueryService<FrolfGroup> frolfGroupQuery;

        public AddCourseCommandValidation(IQueryService<FrolfGroup> frolfGroupQuery, IWorkUnit workUnit)
            : base(workUnit)
        {
            this.frolfGroupQuery = frolfGroupQuery;
        }

        public override void OnPreHandleCommand(AddCourseCommand command)
        {
            var course = command.newEntity;

            // todo: move this to a separate validator?
            Assert(!string.IsNullOrWhiteSpace(course.Name), "Course name must be set");

            AssertFrolfGroupExists(course);
            AssertCurrentUserInFrolfGroup(course);
            AssertCourseHolesSet(course);
            AssertCourseHolePars(course);
            AssertCourseHoleTeeNoDuplicate(course);
            AssertCourseHoleTeeSequence(course);
        }

        private void AssertFrolfGroupExists(Course course)
        {
            var frolfGroupExists = frolfGroupQuery.GetAll().Any(e => e.EntityKey == course.FrolfGroupId);

            Assert(frolfGroupExists, "Frolf group does not exist");
        }

        private void AssertCurrentUserInFrolfGroup(Course course)
        {
            var frolfGroup      = frolfGroupQuery.GetAll().SingleOrDefault(e => e.EntityKey == course.FrolfGroupId);
            var currentUserId   = UserExtensions.GetCurrentUserId();

            var userInGroup     = frolfGroup != null && frolfGroup.Members.Any(m => m.AppUserId == currentUserId);

            Assert(userInGroup, "Frolf group does not exist");
        }

        private void AssertCourseHolesSet(Course course)
        {
            Assert(course.Holes != null, "Course holes must be set");
        }

        private void AssertCourseHolePars(Course course)
        {
            var parsAboveOne = !course.Holes.Any(h => h.Par < 1);
            Assert(parsAboveOne, "Course hole pars must be more than 1");
        }

        private void AssertCourseHoleTeeNoDuplicate(Course course)
        {
            var holeOrders      = course.Holes.Select(h => h.Order);
            var uniqueOrders    = holeOrders.Distinct().OrderBy(e => e);

            var duplicateOrders = holeOrders.Count() != uniqueOrders.Count();


            Assert(!duplicateOrders, "No duplicate hole orders");
        }

        private void AssertCourseHoleTeeSequence(Course course)
        {
            var uniqueTees      = course.Holes.Distinct().OrderBy(e => e.Order).Select(e => e.Order);
            var expectedTees    = new HashSet<int>();

            var expectedTee = 1;
            foreach (var order in uniqueTees)
            {
                expectedTees.Add(expectedTee++);
            }

            var s = new List<string>();
            var x = new List<string>();

            var a = s.SequenceEqual(x);

            var startsAtOne     = uniqueTees.First() == 1;
            var sequenceMatches = uniqueTees.SequenceEqual(expectedTees.AsEnumerable());

            var isValid = startsAtOne && sequenceMatches;

            Assert(isValid, "The sequence is out of place");
        }
    }

    public class AddCourseCommandHandler : CommandHandler<AddCourseCommand>
    {
        public AddCourseCommandHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        protected override void OnHandleCommand(AddCourseCommand command)
        {
            GetRepository<Course>().Add(command.newEntity);
            AddHoles(command.newEntity);
        }

        private void AddHoles(Course course)
        {
            var holeRepo = GetRepository<Hole>();

            foreach (var hole in course.Holes)
            {
                hole.CourseId = course.EntityKey;
                holeRepo.Add(hole);
            }
        }
    }
}
