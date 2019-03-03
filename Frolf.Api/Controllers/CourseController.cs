using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/courses")]
    public class CourseController : EditControllerBase<CourseModel, CourseFilterModel>
    {
        private readonly ICourseModelDataController courseGroupDataController;

        public CourseController(
            ICourseModelDataController courseGroupDataController)
        {
            this.courseGroupDataController = courseGroupDataController;
        }

        public override Task<IEnumerable<CourseModel>> GetAll()
        {
            var foundCourses = courseGroupDataController.GetAll();

            return Task.FromResult(foundCourses);
        }

        public override Task<CourseModel> GetById([FromUri] Guid id)
        {
            var foundFrolfGroup = courseGroupDataController.GetById(id);

            return Task.FromResult(foundFrolfGroup);
        }

        protected override Task<CourseModel> Create([FromBody] CourseModel newEntity)
        {
            courseGroupDataController.Insert(newEntity);

            return Task.FromResult(newEntity);
        }

        protected override Task Remove([FromUri] Guid id)
        {
            throw new NotImplementedException();
        }

        protected override Task Update([FromBody] CourseModel newDetails)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<CourseModel>> GetWithFilter([FromBody] CourseFilterModel filter)
        {
            throw new NotImplementedException();
        }
    }
}