using Domain.Entities.Contracts;
using System;

namespace Domain.Entities
{
    /// <summary>
    /// Represent a frolf group invitation for a user.
    /// </summary>
    public class FrolfGroupInvite : IGuidEntity, IOwnable
    {
        public FrolfGroupInvite()
        {

        }

        public Guid EntityKey { get; set; }
        public Guid AppUserId { get; set; }
        public Guid FrolfGroupId { get; set; }
        public Guid CreatedBy { get; set; }
        public InviteState status { get; set; }


        public virtual AppUser AppUser { get; set; }
        public virtual FrolfGroup FrolfGroup { get; set; }
    }
}
