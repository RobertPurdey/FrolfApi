using Security.Contracts;
using System;
using System.Security.Cryptography;

namespace Security.Encryption
{
    public class SaltShaker : ISaltShaker
    {
        /// <summary>
        /// Dispenses salt.
        /// </summary>
        /// <param name="duration">How long to dispense salt</param>
        /// <returns>Secure salt</returns>
        public string Shake(int duration)
        {
            using ( RandomNumberGenerator rng = new RNGCryptoServiceProvider() )
            {
                var tokenData = new byte[duration];
                rng.GetBytes(tokenData);

                return Convert.ToBase64String(tokenData);
            }
        }
    }
}
