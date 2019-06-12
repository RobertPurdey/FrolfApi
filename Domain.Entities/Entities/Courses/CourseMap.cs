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

            Property(c => c.FrolfGroupId)
                .HasColumnName("frolf_group_id")
                .IsRequired();

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");

            // Relationships

            // - Link to frolf group
            HasRequired(c => c.FrolfGroup)
                .WithMany(fg => fg.Courses)
                .HasForeignKey(c => c.FrolfGroupId);
        }
    }
}
