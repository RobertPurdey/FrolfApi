using System;
using Frolf.Api.Models.Encryption;
using Newtonsoft.Json;
using Security.Contracts;

namespace Frolf.Api.Encryption
{
    public class ModelEncryptor : IModelEncryptor
    {
        private readonly IRsaEncryptionManager rsa;
        private readonly IAesEncryptionManager aes;
        private readonly IRsaPrivateKeyInfo rsaKeyInfo;

        public ModelEncryptor(
            IRsaEncryptionManager rsaManager,
            IAesEncryptionManager aesManager,
            IRsaPrivateKeyInfo serverKeyInfo)
        {
            rsa         = rsaManager;
            aes         = aesManager;
            rsaKeyInfo  = serverKeyInfo;
        }

        public EncryptModel Encrypt<TModel>(string xmlRsaClientKey, TModel model) where TModel : class
        {
            string aesKey = aes.GenerateKey();

            byte[] encryptedAesKey       = rsa.Encrypt( xmlRsaClientKey, aesKey );
            string encryptedJsonBase64   = aes.Encrypt( aesKey, JsonConvert.SerializeObject(model) );
            string encryptedAesKeyBase64 = Convert.ToBase64String(encryptedAesKey);

            return new EncryptModel
            {
                EncryptedAesKey = encryptedAesKeyBase64,
                EncryptedJson   = encryptedJsonBase64
            };
        }

        public TModel Decrypt<TModel>(EncryptModel model) where TModel : class
        {
            var encryptedAesKeyBytes  = Convert.FromBase64String(model.EncryptedAesKey);
            var serverRsaPrivKey      = rsaKeyInfo.GetRsaPrivateKeyXml();

            var decrytedAesKeyBytes  = rsa.Decrypt(serverRsaPrivKey, encryptedAesKeyBytes);
            var decrytedAesKeyBase64 = Convert.ToBase64String(decrytedAesKeyBytes);
            var decryptedJsonBytes   = aes.Decrypt(decrytedAesKeyBase64, model.EncryptedJson);

            return JsonConvert.DeserializeObject<TModel>(decryptedJsonBytes);
        }
    }
}