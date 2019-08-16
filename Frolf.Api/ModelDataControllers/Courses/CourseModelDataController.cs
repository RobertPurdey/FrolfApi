using Application.Command.Courses.Commands;
using Application.Query.Services.Courses;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Mappers;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.Courses;
using Frolf.Api.Models.Holes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Frolf.Api.ModelDataControllers.Courses
{
    public class CourseModelDataController :
        ModelDataController<CourseModel, CourseFilterModel>,
        ICourseModelDataController
    {
        private readonly IReadWriteEntityMapper<CourseModel, Course> courseMapper;
        private readonly IReadWriteEntityMapper<HoleModel, Hole> holeMapper;

        private readonly IQueryService<Hole> holeQueryService;
        private readonly IQueryService<Course> CourseQueryService;
        private readonly IQueryService<Player> playerQueryService;

        private readonly ICommandExecutor commandExecutor;
        
        public CourseModelDataController(
            IReadWriteEntityMapper<CourseModel, Course> courseMapping,
            IReadWriteEntityMapper<HoleModel, Hole> holeMapping,
            IQueryService<Course> CourseService,
            IQueryService<Hole> holeQueryService,
            IQueryService<Player> playerQueryService,
            ICommandExecutor cmdExecutor)
        {
            courseMapper            = courseMapping;
            holeMapper              = holeMapping;
            CourseQueryService      = CourseService;
            this.holeQueryService   = holeQueryService;
            this.playerQueryService = playerQueryService;
            commandExecutor         = cmdExecutor;
        }

        public override void Delete(CourseModel modelToDelete)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<CourseModel> GetAll()
        {
            foreach ( var entity in CourseQueryService.GetAll() )
            {
                var model = new CourseModel();
                courseMapper.MapToApiModel(model, entity);

                yield return model;
            }       
        }

        public override IEnumerable<CourseModel> GetWithFilter(CourseFilterModel filter)
        {
           // AssertCanMakeFilterCall(filter);

            var queryArg = ConvertToQueryArg(filter);

            foreach ( var entity in CourseQueryService.GetWithQueryArg(queryArg) )
            {
                var model = new CourseModel();
                courseMapper.MapToApiModel(model, entity);

                yield return model;
            }
        }
        
        /// <summary>
        /// Can make filter call if the guid being used is not null
        /// </summary>
        /// <param name="frolfGroupId"></param>
        private void AssertCanMakeFilterCall(CourseFilterModel filter)
        {
            if ( !filter.FrolfGroupId.HasValue ) throw new Exception("Cannot make filter call with null frolf group id");

            var currentUserGuid = UserExtensions.GetCurrentUserId();

            var isCurrentUserInGroup = playerQueryService.GetAll().Any(
                p => p.AppUserId     == currentUserGuid
                  && p.FrolfGroupId  == filter.FrolfGroupId);

            // Must be in group to make query
            if ( !isCurrentUserInGroup )
            { 
                ThrowHttpResponseException(
                    "You can't access groups you don't belong in.",
                     HttpStatusCode.Unauthorized);
            }
        }

        public override CourseModel GetById(Guid id)
        {
            var entity = FindEntity(id, CourseQueryService);
            var model  = new CourseModel();

            courseMapper.MapToApiModel(model, entity);
            MapCourseHolesToModels(model, entity);

            return model;
        }

        /// <summary>
        /// Maps course holes to the course model holes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="course"></param>
        private void MapCourseHolesToModels(CourseModel model, Course course)
        {
            var mappedHoles = new List<HoleModel>();
            var holeIds     = course.Holes.Select(h => h.EntityKey);

            var courseHoles = holeQueryService.GetAll().Where(
                h => holeIds.Contains(h.EntityKey));

            foreach ( var hole in courseHoles.OrderBy(h => h.Order) )
            {
                var holeModel = new HoleModel();

                holeMapper.MapToApiModel(holeModel, hole);
                mappedHoles.Add(holeModel);
            }

            model.Holes = mappedHoles;
        }

        /// <summary>
        /// Maps course holes to the course model holes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="course"></param>
        private void MapCourseHoleModels(CourseModel model, Course course)
        {
            var mappedHoles = new List<Hole>();

            foreach (var holeModel in model.Holes)
            {
                var hole = new Hole();

                holeMapper.MapToEntity(holeModel, hole);
                mappedHoles.Add(hole);
            }

            course.Holes = mappedHoles;
        }

        public override void Insert(CourseModel newModel)
        {
            var newEntity = new Course();

            courseMapper.MapToEntity(newModel, newEntity);
            MapCourseHoleModels(newModel, newEntity);

            commandExecutor.Execute(new AddCourseCommand { newEntity = newEntity } );

            MapCourseHolesToModels(newModel, newEntity);
        }

        public override void Update(CourseModel modelToUpdate)
        {
            var entity = FindEntity(modelToUpdate.IdKey, CourseQueryService);

            // only let the name be changed
            entity.Name = modelToUpdate.Name;

            commandExecutor.Execute(new UpdateCourseCommand {  entity = entity } );
        }

        private static CourseQueryArg ConvertToQueryArg(CourseFilterModel filter)
        {
            return new CourseQueryArg
            {
                FrolfGroupId = filter.FrolfGroupId
            };
        }
    }
}