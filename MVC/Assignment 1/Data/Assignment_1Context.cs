using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment_1.Models;

namespace Assignment_1.Data
{
    public class Assignment_1Context : DbContext
    {
        public Assignment_1Context (DbContextOptions<Assignment_1Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Assignment_1.Models.Game> Game { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    GameId= 1,
                    Title = "Fifa",
                    Developer = "EA Sports",
                    Genre = "FPS",
                    ReleaseYear = 2019,
                    PurchaseDate= DateTime.Now,
                    Rating=10,
                });
        }
    }
}
