using Domain.Entities;
using Frolf.Api.Models.Courses;
using System.Linq;

namespace Frolf.Api.Mappers.Courses
{
    public class CourseMapper : IReadOnlyEntityMapper<CourseModel, Course>
    {
        public CourseMapper()
        {

        }

        // todo: map more for invite/decline invite
        public void MapToApiModel(CourseModel apiModel, Course entity)
        {
            apiModel.IdKey           = entity.EntityKey;
            apiModel.Name            = entity.Name;
            apiModel.Par             = entity.Holes.Sum( h => h.Par );
            apiModel.HoleCount       = entity.Holes.Count();
        }
    }
}