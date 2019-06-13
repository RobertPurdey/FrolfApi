using Domain.Entities;
using Domain.Query;
using System;
using System.Linq.Expressions;

namespace Application.Query.Services.Courses
{
    public class CourseQueryArg : GuidEntityQueryArg<Course>
    {

        public Guid? FrolfGroupId { get; set; }

        protected override Expression<Func<Course, bool>> ConstructFilter()
        {
            return HasFrolfGroupAssociation();
        }

        private Expression<Func<Course, bool>> HasFrolfGroupAssociation()
        {
            return FrolfGroupId.HasValue
                ? e => e.FrolfGroupId == FrolfGroupId
                : True;
        }
    }
}

