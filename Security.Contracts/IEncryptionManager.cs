namespace Security.Contracts
{
    /// <summary>
    /// Defines capabilities for encrypting, decrypting and hashing strings.
    /// </summary>
    public interface IEncryptionManager
    {
        string Encrypt(byte[] key, string input);
        string Hash(string input);
        string Decrypt(byte[] key, string input);
    }
}
