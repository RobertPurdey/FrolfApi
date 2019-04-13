using System.Data.Entity.ModelConfiguration;

namespace Domain.Entities
{
    public class FrolfGroupMap : EntityTypeConfiguration<FrolfGroup>
    {
        public FrolfGroupMap()
        {
            ToTable("frolf_group");

            // Primary key
            HasKey(fg => fg.EntityKey);

            // Properties
            Property(t => t.EntityKey)
                .HasColumnName("id");

            Property(fg => fg.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");

            Property(fg => fg.CreatedBy)
                .IsRequired()
                .HasColumnName("created_by");

            Property(fg => fg.CreatedDate)
                .HasColumnName("created_date");
        }
    }
}
