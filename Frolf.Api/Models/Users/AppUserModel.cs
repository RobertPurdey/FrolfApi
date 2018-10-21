using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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