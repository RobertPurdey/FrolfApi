using System;

namespace Security.Contracts
{
    public interface IRefreshToken
    {
        Guid Id { get; set; }
        Guid UserId { get; set; }
        string Token { get; set; }
        string SerializedTicket { get; set; }
        DateTime IssuedOn { get; set; }
        DateTime ExpiresOn { get; set; }
    }
}
