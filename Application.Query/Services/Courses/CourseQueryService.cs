using Domain.Entities;
using Domain.Entities.Contracts;
using Domain.Query;

namespace Application.Query.Services.Courses
{
    public class CourseQueryService : QueryService<Course>
    {
        public CourseQueryService(IEntityDbContext context)
            : base(context)
        {

        }
    }
}
