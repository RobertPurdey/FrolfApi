using Domain.Entities.Contracts;
using Domain.Query.Contracts;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Frolf.Api.ModelDataControllers
{
    public abstract class ModelDataController<TApiModel, TFilterModel> : IModelDataController<TApiModel, TFilterModel>
        where TApiModel : class, IApiDataModel
        where TFilterModel : class, IFilterModel
    {
        public abstract void Delete(TApiModel modelToDelete);
        public abstract IEnumerable<TApiModel> GetAll();
        public abstract TApiModel GetById(Guid id);
        public abstract IEnumerable<TApiModel> GetWithFilter(TFilterModel filter);
        public abstract void Insert(TApiModel newModel);
        public abstract void Update(TApiModel modelToUpdate);

        protected TEntity FindEntity<TEntity>(Guid entityKey, IQueryService<TEntity> queryService)
            where TEntity : class, IGuidEntity
        {
            var entity = queryService
                .GetAll()
                .SingleOrDefault(u => u.EntityKey == entityKey);

            if (entity == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content    = new StringContent($"Entity type: {typeof(TEntity)} was not found using the following key: {entityKey}"),
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            return entity;
        }
    }
}