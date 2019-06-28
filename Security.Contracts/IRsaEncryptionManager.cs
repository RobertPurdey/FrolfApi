namespace Security.Contracts
{
    public interface IRsaEncryptionManager
    {
        byte[] Encrypt(string xmlKey, string msg);
        byte[] Decrypt(string xmlKey, byte[] encryptedMsg);
    }
}
