using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Contracts
{
    /// <summary>
    /// Defines capabilities for encrypting, decrypting and hashing strings.
    /// </summary>
    public interface IEncryptionManager
    {
        string Encrypt(string input);
        string Hash(string input);
        string Decrypt(string input);
    }
}
