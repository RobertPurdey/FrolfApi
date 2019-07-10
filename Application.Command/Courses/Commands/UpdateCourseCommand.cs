using Application.Command.Courses.Conditions;
using Application.Command.FrolfGroups.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using System.Linq;

namespace Application.Command.Courses.Commands
{
    public class UpdateCourseCommand : ICommand
    {
        public Course entity { get; set; }
    }

    public class UpdateCourseCommandValidation : CommandPreHandler<UpdateCourseCommand>
    {
        private readonly IQueryService<FrolfGroup> frolfGroupQuery;

        public UpdateCourseCommandValidation(
            IQueryService<FrolfGroup> frolfGroupQuery,
            IWorkUnit workUnit)
            : base(workUnit)
        {
            this.frolfGroupQuery = frolfGroupQuery;
        }

        public override void OnPreHandleCommand(UpdateCourseCommand command)
        {
            var course = command.entity;

            // todo: move this to a separate validator?
            Assert(!string.IsNullOrWhiteSpace(course.Name), "Course name must be set");

            AssertFrolfGroupExists(course);
            AssertCurrentUserInFrolfGroup(course);
            AssertCourseHolesSet(course);
            AssertCourseHolePars(course);
            AssertCourseHoleOrderUnique(course);
            AssertCourseHoleTeeSequence(course);
        }

        private void AssertFrolfGroupExists(Course course)
        {
            var foundFrolfGroup = frolfGroupQuery
                .GetAll()
                .SingleOrDefault(e => e.EntityKey == course.FrolfGroupId);

            var isValid = !new IsFrolfGroupNull().Validate(foundFrolfGroup);

            Assert(isValid, "Frolf group does not exist");
        }

        private void AssertCurrentUserInFrolfGroup(Course course)
        {
            var frolfGroup      = frolfGroupQuery.GetAll().SingleOrDefault(e => e.EntityKey == course.FrolfGroupId);
            var currentUserId   = UserExtensions.GetCurrentUserId();

            var isValid = new IsUserAGroupMember(currentUserId).Validate(frolfGroup);

            Assert(isValid, "User not a member");
        }

        private void AssertCourseHolesSet(Course course)
        {
            var isValid = !new AreCourseHolesNull().Validate(course);

            Assert(isValid, "Course holes must be set");
        }

        private void AssertCourseHolePars(Course course)
        {
            var isValid = new DoCourseHolesMeetParRequirement().Validate(course);

            Assert(isValid, "Course hole pars must be more than 1");
        }

        private void AssertCourseHoleOrderUnique(Course course)
        {
            var isValid = new IsCourseHolesOrderUnique().Validate(course);


            Assert(isValid, "No duplicate hole orders");
        }

        private void AssertCourseHoleTeeSequence(Course course)
        {
            var isValid = new IsCourseHolesOrderSequenced().Validate(course);

            Assert(isValid, "The sequence is out of place");
        }
    }

    public class UpdateCourseCommandHandler : CommandHandler<UpdateCourseCommand>
    {
        public UpdateCourseCommandHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        protected override void OnHandleCommand(UpdateCourseCommand command)
        {
            GetRepository<Course>().Update(command.entity);
        }
    }
}