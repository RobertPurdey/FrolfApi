using System.Data.Entity.ModelConfiguration;

namespace Domain.Entities
{
    public class GameMap : EntityTypeConfiguration<Game>
    {
        public GameMap()
        {
            ToTable("game");

            // Primary key
            HasKey(g => g.EntityKey);

            // Properties
            Property(g => g.EntityKey)
                .HasColumnName("id");

            Property(g => g.FrolfGroupId)
                .IsRequired()
                .HasColumnName("frolf_group_id");

            Property(g => g.CourseId)
                .IsRequired()
                .HasColumnName("course_id");

            Property(g => g.CreatedBy)
                .IsRequired()
                .HasColumnName("created_by");

            Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");

            Property(g => g.State)
                .IsRequired()
                .HasColumnName("state");

            Property(g => g.CreatedDate)
                .HasColumnName("created_date");

            // Relationships
            // - Link to Course
            HasRequired(g => g.Course)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.CourseId);

            // - Link to Frolf group
            HasRequired(g => g.FrolfGroup)
                .WithMany(p => p.Games)
                .HasForeignKey(hs => hs.FrolfGroupId);
        }
    }
}
