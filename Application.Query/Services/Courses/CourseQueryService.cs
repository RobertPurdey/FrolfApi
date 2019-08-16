using Domain.Entities;
using Domain.Entities.Contracts;
using Domain.Query;
using System.Linq;

namespace Application.Query.Services.Courses
{
    public class CourseQueryService : QueryService<Course>
    {
        public CourseQueryService(IEntityDbContext context)
            : base(context)
        {

        }

        public override IQueryable<Course> GetAll()
        {
            var query        = base.GetAll();
            var currUserGuid = UserExtensions.GetCurrentUserId();

            return query.Where(
                c => c.FrolfGroup.Members.Any(m => m.AppUserId == currUserGuid));
        }
    }
}
