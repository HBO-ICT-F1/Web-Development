using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web_Development.Models;

namespace Web_Development
{
    /// <summary>
    ///     Class used for handling the database
    /// </summary>
    public class Database : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

        /// <summary>
        ///     Creates the database if it doesn't exist yet
        /// </summary>
        /// <param name="host">The host instance to create the database for</param>
        /// <returns>Whether a new database was created or not</returns>
        public static bool Create(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                // Find database instance
                var context = services.GetRequiredService<Database>();

                // Create database if it doesn't exist
                if (!context.Database.EnsureCreated()) return true;
                Console.WriteLine("Created database");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create database: {0}", e.Message);
            }

            return false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: Set connect string
            optionsBuilder.UseMySQL("");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create tables
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Record>().ToTable("records");
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Sale>().ToTable("sales");
        }
    }
}