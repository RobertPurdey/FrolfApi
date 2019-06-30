using System;

namespace Frolf.Api.Models.Players
{
    public class RemovePlayerModel
    {
        public Guid FrolfGroupId { get; set; }
        public Guid PlayerId { get; set; }
    }
}