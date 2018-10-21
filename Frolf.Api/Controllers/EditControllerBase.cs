using Frolf.Api.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    /// <summary>
    /// Provides API calls to perform CRUD operations on an entity.
    /// </summary>
    /// <typeparam name="TApiModel">Model corresponding to the entity being operated on.</typeparam>
    public abstract class EditControllerBase<TApiModel> : ControllerBase
        where TApiModel : class, IApiDataModel
    {
        public EditControllerBase()
        {

        }

        protected abstract Task<TApiModel> Create([FromBody] TApiModel newEntity);

        protected abstract Task Remove([FromUri] Guid id);

        protected abstract Task Update([FromBody] TApiModel newDetails);

        [HttpGet]
        [Route("{id:guid}")]
        public abstract Task<TApiModel> GetById([FromUri] Guid id);

        [HttpPost]
        [Route("")]
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