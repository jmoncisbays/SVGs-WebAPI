using Microsoft.EntityFrameworkCore;

namespace SVGsWebAPI.Models
{
    public class SVGsContext : DbContext
    {
        public virtual DbSet<SVG> SVGs { get; set; }

        public SVGsContext(DbContextOptions<SVGsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SVG>(entity =>
            {
                entity.ToTable("SVGs");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("Title")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Specification)
                    .IsRequired()
                    .HasColumnName("Specification")
                    .IsUnicode(false);

                entity.Property(e => e.PNG)
                    .HasColumnName("PNG");
            });
        }
    }
}
