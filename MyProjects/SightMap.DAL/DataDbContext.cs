using Microsoft.EntityFrameworkCore;
using SightMap.DAL.Models;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace SightMap.DAL
{
    public class DataDbContext : DbContext
    {
        // Конструктор для DI
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

        // Конструктор для самостоятельного создания контекста
        public DataDbContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DALConstants.ConnectionString,
                options => options.UseNetTopologySuite());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sight>(entity =>
            {
                entity.HasOne<SightType>(s => s.Type)
                      .WithMany()
                      .HasForeignKey(s => s.SightTypeId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(nameof(Sight.SightTypeId)).HasColumnName("Type");

                entity.Property(s => s.Coordinates).HasColumnName("Coordinates");//.HasColumnType("geography (point)");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne<Sight>()
                      .WithMany()
                      .HasForeignKey(r => r.ItemId)
                      .OnDelete(DeleteBehavior.Cascade);

                //entity.HasMany<Review>()
                //      .WithOne()
                //      .HasForeignKey(r => r.ParentId);
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasOne<Sight>()
                      .WithMany()
                      .HasForeignKey(a => a.ItemId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
