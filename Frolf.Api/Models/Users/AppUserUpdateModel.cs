using Frolf.Api.Models.Contracts;
using System;

namespace Frolf.Api.Models.Users
{
    public class AppUserUpdateModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public string LoginName { get; set; }
        public string Handle { get; set; }
    }
}