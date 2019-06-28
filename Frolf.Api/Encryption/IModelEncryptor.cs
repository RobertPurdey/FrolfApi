using Frolf.Api.Models.Encryption;

namespace Frolf.Api.Encryption
{
    public interface IModelEncryptor
    {
        EncryptModel Encrypt<T>(string xmlClientRsaKey, T model) where T : class;
        T Decrypt<T>(EncryptModel model) where T : class;
    }
}