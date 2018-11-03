using System.Data.Entity.ModelConfiguration;

namespace Domain.Entities.Entities.FrolfGroups
{
    public class FrolfGroupInviteMap : EntityTypeConfiguration<FrolfGroupInvite>
    {
        public FrolfGroupInviteMap()
        {
            ToTable("frolf_group_invite");

            // Primary key
            HasKey(e => e.EntityKey);

            // Properties
            Property(e => e.EntityKey)
                .HasColumnName("id");

            Property(e => e.AppUserId)
                .IsRequired()
                .HasColumnName("app_user_id");

            Property(e => e.FrolfGroupId)
                .IsRequired()
                .HasColumnName("frolf_group_id");

            Property(e => e.CreatedBy)
                .IsRequired()
                .HasColumnName("created_by");

            Property(e => e.status)
                .IsRequired()
                .HasColumnName("status");

            // Relationships
            HasRequired(p => p.AppUser)
                .WithMany(u => u.GroupInvites)
                .HasForeignKey(p => p.AppUserId);

            HasRequired(p => p.AppUser)
                .WithMany(u => u.SentInvites)
                .HasForeignKey(p => p.CreatedBy);

            HasRequired(p => p.FrolfGroup)
                .WithMany(fg => fg.Invites)
                .HasForeignKey(p => p.FrolfGroupId);
        }
    }
}
