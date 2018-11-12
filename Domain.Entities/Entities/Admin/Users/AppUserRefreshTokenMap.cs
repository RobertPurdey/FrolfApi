using System.Data.Entity.ModelConfiguration;

namespace Domain.Entities
{
    public class AppUserRefreshTokenMap : EntityTypeConfiguration<AppUserRefreshToken>
    {
        public AppUserRefreshTokenMap()
        {
            ToTable("app_user_refresh_token");

            // Primary key
            HasKey(e => e.Id);

            // Properties
            Property(e => e.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            Property(e => e.Token)
                .HasColumnName("token")
                .IsRequired()
                .HasMaxLength(100);

            Property(e => e.SerializedTicket)
                .HasColumnName("serialized_ticket")
                .IsRequired();

            Property(e => e.IssuedOn)
                .HasColumnName("issued_on")
                .IsRequired();

            Property(e => e.ExpiresOn)
                .HasColumnName("expires_on")
                .IsRequired();

            // Ignores
            Ignore(e => e.EntityKey);
        }
    }
}
