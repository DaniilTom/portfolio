﻿using Microsoft.EntityFrameworkCore;
using SightMap.DAL.Models;

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
            optionsBuilder.UseSqlServer(DALConstants.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sight>(entity =>
            {
                entity.HasOne<SightType>(s => s.Type)
                      .WithOne()
                      .HasForeignKey<Sight>(s => s.SightTypeId);

                entity.Property(nameof(Sight.SightTypeId)).HasColumnName("Type");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne<Sight>()
                      .WithOne()
                      .HasForeignKey<Review>(r => r.ItemId);
            });
        }
    }
}
