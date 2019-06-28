using Frolf.Api.ControllerAttributes;
using Frolf.Api.Encryption;
using Frolf.Api.ModelDataControllers.Users;
using Frolf.Api.Models.Encryption;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [Authorize]
    [AppUserLoginAuthorizationFilter]
    public class ControllerBase : ApiController
    {
        private readonly IModelEncryptor modelEncryptor;
        private readonly IAppUserPublicKeyRetriever userRsaKeyRetriever;

        public ControllerBase(
            IModelEncryptor modelEncryptor,
            IAppUserPublicKeyRetriever userRsaKeyRetriever)
        {
            this.modelEncryptor      = modelEncryptor;
            this.userRsaKeyRetriever = userRsaKeyRetriever;
        }

        protected EncryptModel EncryptModel<T>(T modelToEncrypt) where T : class
        {
            return modelEncryptor.Encrypt(userRsaKeyRetriever.GetXmlRsaPublicKey(), modelToEncrypt);
        }

        protected T DecryptModel<T>(EncryptModel model) where T : class
        {
            return modelEncryptor.Decrypt<T>(model);
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