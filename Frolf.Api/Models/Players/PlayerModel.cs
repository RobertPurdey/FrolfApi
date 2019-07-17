using Domain.Entities;
using Frolf.Api.Models.Contracts;
using System;

namespace Frolf.Api.Models.Players
{
    public class PlayerModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public Guid AppUserId { get; set; }
        public Guid FrolfGroupId { get; set; }
        public GroupRole GroupRole { get; set; }
        public string Handle { get; set; }
    }

    public class PlayerFilterModel : IFilterModel
    {

    }
}