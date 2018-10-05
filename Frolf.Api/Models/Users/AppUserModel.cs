using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frolf.Api.Models.Users
{
    public class AppUserModel : IApiDataModel
    {
        public Guid Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}