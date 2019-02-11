using System.Data.Entity.ModelConfiguration;

namespace Domain.Entities
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

            Property(e => e.InviteeId)
                .IsRequired()
                .HasColumnName("invitee_id");

            Property(e => e.FrolfGroupId)
                .IsRequired()
                .HasColumnName("frolf_group_id");

            Property(e => e.InviterId)
                .IsRequired()
                .HasColumnName("inviter_id");

            // Relationships
            HasRequired(p => p.Invitee)
                .WithMany(u => u.GroupInvites)
                .HasForeignKey(p => p.InviteeId);

            HasRequired(p => p.Inviter)
                .WithMany(u => u.SentInvites)
                .HasForeignKey(p => p.InviterId);

            HasRequired(p => p.FrolfGroup)
                .WithMany(fg => fg.Invites)
                .HasForeignKey(p => p.FrolfGroupId);
        }
    }
}
