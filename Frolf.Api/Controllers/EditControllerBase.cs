using Frolf.Api.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    /// <summary>
    /// Provides API calls to perform CRUD operations on an entity.
    /// </summary>
    /// <typeparam name="TApiModel">Model corresponding to the entity being operated on.</typeparam>
    public abstract class EditControllerBase<TApiModel, TFilterModel> : ControllerBase
        where TApiModel     : class, IApiDataModel
        where TFilterModel  : class, IFilterModel
    {
        public EditControllerBase()
        {

        }

        protected abstract Task<TApiModel> Create([FromBody] TApiModel newEntity);

        protected abstract Task Remove([FromUri] Guid id);

        protected abstract Task Update([FromBody] TApiModel newDetails);
        
        [HttpGet]
        [Route("")]
        public abstract Task<IEnumerable<TApiModel>> GetAll();

        [HttpPost]
        [Route("filter")]
        public abstract Task<IEnumerable<TApiModel>> GetWithFilter([FromBody] TFilterModel filter);

        [HttpGet]
        [Route("{id:guid}")]
        public abstract Task<TApiModel> GetById([FromUri] Guid id);

        [HttpPost]
        [Route("insert")]
        public async Task<TApiModel> Post([FromBody] TApiModel newEntity)
        {
            var result = await Create(newEntity);

            return result;
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task Delete([FromUri] Guid id)
        {
            await Remove(id);
        }

        [HttpPut]
        [Route("")]
        public async Task Put([FromBody] TApiModel newDetails)
        {
            await Update(newDetails);
        }
    }
}