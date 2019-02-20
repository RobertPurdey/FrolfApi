using System.Data.Entity.ModelConfiguration;

namespace Domain.Entities
{
    public class HoleScoreMap : EntityTypeConfiguration<HoleScore>
    {
        public HoleScoreMap()
        {
            ToTable("hole_score");

            // Primary key
            HasKey(hs => hs.EntityKey);

            // Properties
            Property(hs => hs.EntityKey)
                .HasColumnName("id");

            Property(hs => hs.HoleId)
                .IsRequired()
                .HasColumnName("hole_id");

            Property(hs => hs.PlayerId)
                .IsRequired()
                .HasColumnName("player_id");

            Property(hs => hs.RoundId)
                .IsRequired()
                .HasColumnName("round_id");

            Property(hs => hs.Score)
                .IsRequired()
                .HasColumnName("score");

            // Relationships
            // - Link to Hole
            HasRequired(hs => hs.Hole)
                .WithMany(h => h.HoleScores)
                .HasForeignKey(hs => hs.HoleId);

            // - Link to Player
            HasRequired(hs => hs.Player)
                .WithMany(p => p.HoleScores)
                .HasForeignKey(hs => hs.PlayerId);

            // - Link to Round
            HasRequired(hs => hs.Round)
                .WithMany(r => r.HoleScores)
                .HasForeignKey(hs => hs.RoundId);
        }
    }
}
