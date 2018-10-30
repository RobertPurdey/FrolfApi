using Domain.Entities;
using System;

namespace Frolf.Api.Models.FrolfGroups
{
    public class PlayerModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public Guid AppUserId { get; set; }
        public Guid FrolfGroupId { get; set; }
        public GroupRole GroupRole { get; set; }
        public string Handle { get; set; }
    }
}