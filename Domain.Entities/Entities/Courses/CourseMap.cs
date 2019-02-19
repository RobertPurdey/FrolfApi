using System.Data.Entity.ModelConfiguration;

namespace Domain.Entities
{
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        public CourseMap()
        {
            ToTable("course");

            // Primary key
            HasKey(c => c.EntityKey);

            // Properties
            Property(c => c.EntityKey)
                .HasColumnName("id");

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");

            // Relationships
        }
    }
}
