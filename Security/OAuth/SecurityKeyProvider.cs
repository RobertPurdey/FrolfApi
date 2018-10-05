using Microsoft.IdentityModel.Tokens;
using Security.Contracts;
using System.Text;

namespace Security.OAuth
{
    public class SecurityKeyProvider : ISecurityKeyProvider
    {
        private const string KEY = "lUf7AagxeQA1WNghx2BIXX6PQGo4QwJYauBCyvDzEYMk2l1JhxtRw7jCsKZJKczM6nQXVsOcwnh9FRltCiuXy1wOk04lc1EGEMLNSFY8FJyy3LRXSQqHj3unACnVZLmysmFYz9EyCPVhibTVJ28HiaZupWMVLEXqje4l4wxQj2dpeAHKZNxAReZdTsvEjDGyumvykyQMQEcyz5QfGnj6uIPX2n4SFVIGGmb5qJfQkPtuB3U2e87VrsEUcZuGYBYj";

        /// <summary>
        /// Get a signing key based on internal logic required to generate one
        /// </summary>
        /// <returns>A security key</returns>
        public SecurityKey GetSigningKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
