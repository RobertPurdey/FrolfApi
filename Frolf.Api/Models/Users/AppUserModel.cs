using Frolf.Api.Models.Contracts;
using Newtonsoft.Json;
using System;

namespace Frolf.Api.Models.Users
{
    public class AppUserModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public string LoginName { get; set; }
        public string Handle { get; set; }
        public string FriendCode { get; set; }
        public string RsaPubXml { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }

    public class AppUserFilterModel : IFilterModel
    {

    }
}