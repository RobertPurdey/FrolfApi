using Domain.Entities;
using Domain.Query;
using System;
using System.Linq.Expressions;

namespace Application.Query.Services.Courses
{
    public class CourseQueryArg : GuidEntityQueryArg<Course>
    {
        public Guid CurrentUserGuid { get; set; }

        protected override Expression<Func<Course, bool>> ConstructFilter()
        {
            return base.ConstructFilter();
        }
    }
}

