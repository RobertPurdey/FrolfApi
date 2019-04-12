using System.Data.Entity.ModelConfiguration;

namespace Domain.Entities
{
    public class HoleMap : EntityTypeConfiguration<Hole>
    {
        public HoleMap()
        {
            ToTable("hole");

            // Primary key
            HasKey(h => h.EntityKey);

            // Properties
            Property(h => h.EntityKey)
                .HasColumnName("id");

            Property(h => h.CourseId)
            .IsRequired()
            .HasColumnName("course_id");

            // todo: begin renaming order -> tee
            Property(h => h.Order)
                .IsRequired()
                .HasColumnName("tee");

            Property(h => h.Par)
                .IsRequired()
                .HasColumnName("par");

            // Relationships
            // - Link to Course
            HasRequired(h => h.Course)
                .WithMany(c => c.Holes)
                .HasForeignKey(h => h.CourseId);
        }
    }
}
