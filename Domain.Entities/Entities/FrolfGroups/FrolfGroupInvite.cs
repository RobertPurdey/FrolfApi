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
        public Guid InviteeId { get; set; }
        public Guid FrolfGroupId { get; set; }
        public Guid CreatedBy { get; set; }


        public virtual AppUser Invitee { get; set; }
        public virtual AppUser Inviter { get; set; }
        public virtual FrolfGroup FrolfGroup { get; set; }
    }
}
