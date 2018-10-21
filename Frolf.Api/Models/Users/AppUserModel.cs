using Newtonsoft.Json;
using System;

namespace Frolf.Api.Models.Users
{
    public class AppUserModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public string NickName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public string Email { get; set; }
    }
}