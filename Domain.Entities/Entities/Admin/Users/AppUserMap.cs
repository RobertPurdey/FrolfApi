using System.Data.Entity.ModelConfiguration;

namespace Domain.Entities
{
    /// <summary>
    /// AppUser model mappings
    /// </summary>
    public class AppUserMap : EntityTypeConfiguration<AppUser>
    {
        public AppUserMap()
        {
            ToTable("app_user");

            // Primary key
            HasKey(u => u.EntityKey);

            // Properties
            Property(u => u.EntityKey)
                .HasColumnName("id");

            Property(u => u.LoginName)
                .IsRequired()
                .HasColumnName("login_name");

            Property(u => u.Password)
                .IsRequired()
                .HasColumnName("password");

            Property(u => u.Email)
                .IsRequired()
                .HasColumnName("email");

            Property(u => u.Handle)
                .IsRequired()
                .HasColumnName("handle");

            Property(u => u.FriendCode)
                .IsRequired()
                .HasColumnName("friend_code");

            // Ignore Identity properties
            Ignore(t => t.Name);
            Ignore(t => t.Identity);
            Ignore(t => t.IsAuthenticated);
            Ignore(t => t.AuthenticationType);
        }
    }
}
