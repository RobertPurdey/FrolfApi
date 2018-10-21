using System.Data.Entity.ModelConfiguration;

namespace Domain.Entities
{
    public class PlayerMap : EntityTypeConfiguration<Player>
    {
        public PlayerMap()
        {
            ToTable("player");

            // Primary key
            HasKey(p => p.EntityKey);

            // Properties
            Property(p => p.EntityKey)
                .HasColumnName("id");

            Property(p => p.AppUserId)
                .IsRequired()
                .HasColumnName("app_user_id");

            Property(p => p.GroupRole)
                .IsRequired()
                .HasColumnName("group_role");

            // Relationships
            HasRequired(p => p.AppUser)
                .WithMany(u => u.Players)
                .HasForeignKey(p => p.AppUserId);

            HasMany(player => player.FrolfGroups)
                .WithMany(group => group.GroupMembers)
                .Map(config =>
                {
                    config.MapLeftKey("player_id");
                    config.MapRightKey("frolf_group_id");
                    config.ToTable("frolf_groupee");
                });
        }
    }
}
