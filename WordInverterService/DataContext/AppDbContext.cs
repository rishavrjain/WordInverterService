using Microsoft.EntityFrameworkCore;
using WordInverterService.Models;

namespace WordInverterService.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<WordInversionRecord> WordInversionRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WordInversionRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OriginalRequest).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.InvertedResult).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
