using Frolf.Api.Models.Contracts;
using Newtonsoft.Json;
using System;

namespace Frolf.Api.Models.Users
{
    public class AppUserModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public string Handle { get; set; }
        public string FriendCode { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public string Email { get; set; }
    }

    public class AppUserFilterModel : IFilterModel
    {

    }
}