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
            
            Property(p => p.FrolfGroupId)
                .IsRequired()
                .HasColumnName("frolf_group_id");

            Property(p => p.GroupRole)
                .IsRequired()
                .HasColumnName("group_role");

            Property(p => p.Handle)
                .IsRequired()
                .HasColumnName("handle");

            // Relationships
            // - Link to app user
            HasRequired(p => p.AppUser)
                .WithMany(u => u.Players)
                .HasForeignKey(p => p.AppUserId);

            // - Link to frolf group
            HasRequired(p => p.FrolfGroup)
                .WithMany(fg => fg.Members)
                .HasForeignKey(p => p.FrolfGroupId);



            // todo: keep this as reference to auto associative table
            //HasMany(player => player.FrolfGroups)
            //    .WithMany(group => group.GroupMembers)
            //    .Map(config =>
            //    {
            //        config.MapLeftKey("player_id");
            //        config.MapRightKey("frolf_group_id");
            //        config.ToTable("frolf_groupee");
            //    });
        }
    }
}
