namespace Security.Contracts
{
    /// <summary>
    /// Defines capabilities for encrypting, decrypting and hashing strings.
    /// </summary>
    public interface IAesEncryptionManager
    {
        string GenerateKey();
        string Encrypt(string key, string msg);
        string Decrypt(string key, string encryptedMsg);
    }
}
