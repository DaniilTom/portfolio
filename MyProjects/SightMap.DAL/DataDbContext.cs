using Microsoft.EntityFrameworkCore;
using SightMap.DAL.Models;

namespace SightMap.DAL
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sight>(entity =>
            {
                entity.HasOne<SightType>(s => s.Type)
                      .WithOne()
                      .HasForeignKey<Sight>(s => s.SightTypeId);

                entity.Property(nameof(Sight.SightTypeId)).HasColumnName("Type");
            });
        }
    }
}
