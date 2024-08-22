using Microsoft.EntityFrameworkCore;
using System.IO;
using Test_Project.Database.Configurations;
using Test_Project.Database.Tables;

namespace Test_Project.Database
{
    class TestContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        private static string GetConnectionString()
        {
            string configurationFileName = "database.cfg";
            string configurationFilePath = Path.Combine(Directory.GetCurrentDirectory(), configurationFileName);
            string[] configurationFileLines = File.ReadAllLines(configurationFilePath);
            string connectionString = string.Join(";", configurationFileLines);

            return connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GetConnectionString());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PhoneConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
        }
    }
}