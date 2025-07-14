using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DataAccess.Entities;
using Microsoft.Extensions.Configuration;

namespace Store.DataAccess.Context
{
    public class StoreContext : DbContext
    {
        public DbSet<GameEntity> Games { get; set; }
        public DbSet<GameGenreEntity> GameGenres { get; set; }
        public DbSet<GamePlatformEntity> GamePlatforms { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<PlatformEntity> Platforms { get; set; }
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameEntity>().HasIndex(g => g.GameKey).IsUnique();

            modelBuilder.Entity<GenreEntity>()
                .HasMany(g => g.SubGenres)
                .WithOne(g => g.ParentGenre)
                .HasForeignKey(g => g.ParentGenreId);

            modelBuilder.Entity<GameGenreEntity>()
                .HasKey(gg => new { gg.GameId, gg.GenreId });

            modelBuilder.Entity<GamePlatformEntity>()
                .HasKey(gp => new { gp.GameId, gp.PlatformId });

        }
    }
}
