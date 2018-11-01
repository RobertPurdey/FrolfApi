namespace Domain.Entities
{
    /// <summary>
    /// Represents the state of an invitation.
    /// </summary>
    public enum InviteState : byte
    {
        Pending = 1,
        Accepted,
        Declined,
    }
}
