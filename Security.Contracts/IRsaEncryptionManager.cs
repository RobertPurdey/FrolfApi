using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Contracts
{
    public interface IRsaEncryptionManager
    {
        byte[] GenerateKeys();
    }
}
