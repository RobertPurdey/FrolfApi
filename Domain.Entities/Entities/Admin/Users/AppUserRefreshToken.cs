using Domain.Entities.Contracts;
using Security.Contracts;
using System;

namespace Domain.Entities
{
    public class AppUserRefreshToken : IRefreshToken, IGuidEntity
    {
        public Guid EntityKey
        { 
            get { return Id; }
            set { Id = value; }
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string SerializedTicket { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
