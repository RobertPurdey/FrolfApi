using System;

namespace Domain.Entities.Contracts
{
    /// <summary>
    /// Represents an entity as being identifiable within its entity type by a Guid.
    /// </summary>
    public interface IGuidEntity
    {
        Guid EntityKey { get; set; }
    }
}
