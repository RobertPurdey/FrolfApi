using Domain.Entities;
using Frolf.Api.Models.Contracts;
using System;

namespace Frolf.Api.Models.FrolfGroups
{
    public class FrolfGroupInviteModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public string GroupName { get; set; }
        public string InviterName { get; set ;}
    }

    public class FrolfGroupInviteFilterModel : IFilterModel
    {
        public InviteState? InviteStatus { get; set; }
    }
}