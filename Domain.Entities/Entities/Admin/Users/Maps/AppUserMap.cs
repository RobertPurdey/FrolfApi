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

            // Primary Key
            HasKey(u => u.EntityKey);

            // Properties
            Property(u => u.LoginName).IsRequired();
            Property(u => u.Password).IsRequired();
            Property(u => u.Email).IsRequired();

            // Table + Column Mappings
            Property(u => u.EntityKey).HasColumnName("id");
            Property(u => u.LoginName).HasColumnName("login_name");
            Property(u => u.Password).HasColumnName("password");
            Property(u => u.Email).HasColumnName("email");

            // Ignore Identity properties
            Ignore(t => t.Name);
            Ignore(t => t.Identity);
            Ignore(t => t.IsAuthenticated);
            Ignore(t => t.AuthenticationType);
        }
    }
}
