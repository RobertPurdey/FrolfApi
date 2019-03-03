using Application.Query.Services.Courses;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Mappers;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frolf.Api.ModelDataControllers.Courses
{
    public class CourseModelDataController :
        ModelDataController<CourseModel, CourseFilterModel>,
        ICourseModelDataController
    {
        private readonly IReadOnlyEntityMapper<CourseModel, Course> CourseMapper;

        private readonly IQueryService<Course> CourseQueryService;

        private readonly ICommandExecutor commandExecutor;
        
        public CourseModelDataController(
            IReadOnlyEntityMapper<CourseModel, Course> CourseMapping,
            IQueryService<Course> CourseService,
            ICommandExecutor cmdExecutor)
        {
            CourseMapper         = CourseMapping;
            CourseQueryService   = CourseService;
            commandExecutor      = cmdExecutor;
        }

        public override void Delete(CourseModel modelToDelete)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<CourseModel> GetAll()
        {
            var currUserGuid = UserExtensions.GetCurrentUserId();

            foreach ( var entity in CourseQueryService.GetAll() )
            {
                var model = new CourseModel();
                CourseMapper.MapToApiModel(model, entity);

                yield return model;
            }       
        }

        public override IEnumerable<CourseModel> GetWithFilter(CourseFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public override CourseModel GetById(Guid id)
        {
            var entity = FindEntity(id, CourseQueryService);
            var model  = new CourseModel();

            CourseMapper.MapToApiModel(model, entity);

            return model;
        }

        public override void Insert(CourseModel newModel)
        {
            throw new NotImplementedException();
        }

        public override void Update(CourseModel modelToUpdate)
        {
            throw new NotImplementedException();
        }

        private static CourseQueryArg ConvertToQueryArg(CourseFilterModel filter)
        {
            return new CourseQueryArg
            {
                // todo: mappings filter => query arg
            };
        }
    }
}