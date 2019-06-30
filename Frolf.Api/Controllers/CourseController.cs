using Frolf.Api.Encryption;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.ModelDataControllers.Users;
using Frolf.Api.Models;
using Frolf.Api.Models.Courses;
using Frolf.Api.Models.Encryption;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/courses")]
    public class CourseController : EditControllerBase<CourseModel, CourseFilterModel>
    {
        private readonly ICourseModelDataController courseGroupDataController;

        public CourseController(
            IModelEncryptor modelEncryptor,
            IAppUserPublicKeyRetriever userRsaKeyRetriever,
            ICourseModelDataController courseGroupDataController)
            : base(modelEncryptor, userRsaKeyRetriever)
        {
            this.courseGroupDataController = courseGroupDataController;
        }

        public override Task<EncryptModel> GetAll()
        {
            var foundCourses    = courseGroupDataController.GetAll();
            var encryptCourses  = EncryptModel(foundCourses);

            return Task.FromResult(encryptCourses);
        }

        public override Task<EncryptModel> GetById([FromUri] EncryptModel id)
        {
            var idModel         = DecryptModel<IdModel>(id);
            var foundCourse     = courseGroupDataController.GetById(idModel.IdKey);
            var encryptCourse   = EncryptModel(foundCourse);

            return Task.FromResult(encryptCourse);
        }

        protected override Task<EncryptModel> Create(EncryptModel newEntity)
        {
            var newCourse = DecryptModel<CourseModel>(newEntity);
            courseGroupDataController.Insert(newCourse);
            var encryptCourse = EncryptModel(newCourse);

            return Task.FromResult(encryptCourse);
        }

        protected override Task Remove(EncryptModel id)
        {
            throw new NotImplementedException();
        }

        protected override Task Update(EncryptModel newDetails)
        {
            var course = DecryptModel<CourseModel>(newDetails);
            courseGroupDataController.Update(course);

            return Task.FromResult(1);
        }

        public override Task<EncryptModel> GetWithFilter([FromBody] EncryptModel filter)
        {
            var filterModel     = DecryptModel<CourseFilterModel>(filter);
            var results         = courseGroupDataController.GetWithFilter(filterModel);
            var encryptCourses  = EncryptModel(results);

            return Task.FromResult(encryptCourses);
        }
    }
}