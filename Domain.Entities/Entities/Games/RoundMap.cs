using System.Data.Entity.ModelConfiguration;

namespace Domain.Entities
{
    public class RoundMap : EntityTypeConfiguration<Round>
    {
        public RoundMap()
        {
            ToTable("round");

            // Primary key
            HasKey(r => r.EntityKey);

            // Properties
            Property(r => r.EntityKey)
                .HasColumnName("id");

            Property(r => r.GameId)
                .IsRequired()
                .HasColumnName("game_id");

            Property(r => r.PlayerId)
                .IsRequired()
                .HasColumnName("player_id");

            // Relationships
            // - Link to Player
            HasRequired(r => r.Player)
                .WithMany(p => p.Rounds)
                .HasForeignKey(r => r.PlayerId);

            // - Link to Game
            HasRequired(r => r.Game)
                .WithMany(g => g.Rounds)
                .HasForeignKey(r => r.GameId);
        }
    }
}
