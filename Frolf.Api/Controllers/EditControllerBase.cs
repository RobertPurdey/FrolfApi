using Frolf.Api.Encryption;
using Frolf.Api.ModelDataControllers.Users;
using Frolf.Api.Models.Contracts;
using Frolf.Api.Models.Encryption;
using Security.Contracts;
using System;
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
        public EditControllerBase(
            IModelEncryptor modelEncryptor,
            IRsaKeyInfo serverKeyInfo,
            IAppUserPublicKeyRetriever userRsaKeyRetriever)
            : base(modelEncryptor, serverKeyInfo, userRsaKeyRetriever)
        {

        }

        protected abstract Task<EncryptModel> Create(EncryptModel newEntity);

        protected abstract Task Remove(EncryptModel idModel);

        protected abstract Task Update(EncryptModel newDetails);
        
        [HttpGet]
        [Route("")]
        public abstract Task<EncryptModel> GetAll();

        [HttpPost]
        [Route("filter")]
        public abstract Task<EncryptModel> GetWithFilter([FromBody] EncryptModel filter);

        [HttpPost]
        [Route("getById")]
        public abstract Task<EncryptModel> GetById([FromBody] EncryptModel id);

        [HttpPost]
        [Route("insert")]
        public async Task<EncryptModel> Post([FromBody] EncryptModel newEntity)
        {
            var result = await Create(newEntity);

            return result;
        }

        [HttpPost]
        [Route("delete")]
        public async Task Delete([FromBody] EncryptModel idModel)
        {
            await Remove(idModel);
        }

        [HttpPost]
        [Route("update")]
        public async Task Put([FromBody] EncryptModel newDetails)
        {
            await Update(newDetails);
        }
    }
}