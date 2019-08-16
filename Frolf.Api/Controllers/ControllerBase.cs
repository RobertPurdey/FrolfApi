using Frolf.Api.ControllerAttributes;
using Frolf.Api.Encryption;
using Frolf.Api.ModelDataControllers.Users;
using Frolf.Api.Models.Encryption;
using Security.Contracts;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [Authorize]
    //[RequireHttpsAuthorizationFilter]
    [AppUserLoginAuthorizationFilter]
    public class ControllerBase : ApiController
    {
        private readonly IModelEncryptor modelEncryptor;
        private readonly IRsaKeyInfo serverKeyInfo;
        private readonly IAppUserPublicKeyRetriever userRsaKeyRetriever;

        public ControllerBase(
            IModelEncryptor modelEncryptor,
            IRsaKeyInfo serverKeyInfo,
            IAppUserPublicKeyRetriever userRsaKeyRetriever)
        {
            this.modelEncryptor      = modelEncryptor;
            this.serverKeyInfo       = serverKeyInfo;
            this.userRsaKeyRetriever = userRsaKeyRetriever;
        }

        protected EncryptModel EncryptModel<TModel>(TModel modelToEncrypt) where TModel : class
        {
            return modelEncryptor.Encrypt(userRsaKeyRetriever.GetXmlRsaPublicKey(), modelToEncrypt);
        }

        protected EncryptModel InternalEncryptModel<TModel>(TModel modelToEncrypt) where TModel : class
        {
            return modelEncryptor.Encrypt(serverKeyInfo.GetPublicKeyXml(), modelToEncrypt);
        }

        protected TModel DecryptModel<TModel>(EncryptModel model) where TModel : class
        {
            return modelEncryptor.Decrypt<TModel>(model);
        }

        protected TModel DecryptModelFromServer<TModel>(EncryptModel model) where TModel : class
        {
            return modelEncryptor.DecryptFromServer<TModel>(model);
        }

        protected virtual void ValidateNullArgument(object argument, string message)
        {
            if (argument == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(message)
                });
            }
        }
    }
}