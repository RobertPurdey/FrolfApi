using Frolf.Api.Models.Contracts;
using System;

namespace Frolf.Api.Models.FrolfGroups
{
    public class InviteCreationModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public Guid GroupId { get; set; }
        public string FriendCode { get; set; }
    }
}