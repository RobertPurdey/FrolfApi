namespace Security.Contracts
{
    /// <summary>
    /// Defines capabilities for encrypting, decrypting and hashing strings.
    /// </summary>
    public interface IEncryptionManager
    {
        string Encrypt(string input, byte[] key);
        string Hash(string input);
        string Decrypt(string input, byte[] key);
    }
}
