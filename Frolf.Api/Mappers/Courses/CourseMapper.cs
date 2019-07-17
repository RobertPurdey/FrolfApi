using Domain.Entities;
using Frolf.Api.Models.Courses;
using System.Linq;

namespace Frolf.Api.Mappers.Courses
{
    public class CourseMapper : IReadWriteEntityMapper<CourseModel, Course>
    {
        public CourseMapper()
        {
            
        }

        public void MapToEntity(CourseModel apiModel, Course entity)
        {
            entity.EntityKey        = apiModel.IdKey;
            entity.FrolfGroupId     = apiModel.FrolfGroupId;
            entity.Name             = apiModel.Name;
        }

        // todo: this comment should be deleted, investigate -> map more for invite/decline invite
        public void MapToApiModel(CourseModel apiModel, Course entity)
        {
            apiModel.IdKey           = entity.EntityKey;
            apiModel.FrolfGroupId    = entity.FrolfGroupId;
            apiModel.Name            = entity.Name;
            apiModel.Par             = entity.Holes.Sum( h => h.Par );
            apiModel.HoleCount       = entity.Holes.Count();
            apiModel.HoleIds         = entity.Holes.Select(h => h.EntityKey);
        }
    }
}