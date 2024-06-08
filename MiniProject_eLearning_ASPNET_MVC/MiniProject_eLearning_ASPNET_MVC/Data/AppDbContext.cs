using EducalBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniProject_eLearning_ASPNET_MVC.Models;
using System;

namespace MiniProject_eLearning_ASPNET_MVC.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Information> Informations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
     

            modelBuilder.Entity<Setting>().HasData(
            new Setting
            {
                Id = 1,
                Key = "HeaderLogo",
                Value = "eLEARNING",
                SoftDeleted = false,
                CreatedDate = DateTime.Now
            },
              new Setting
              {
                  Id = 2,
                  Key = "Phone",
                  Value = "+012 345 67890",
                  SoftDeleted = false,
                  CreatedDate = DateTime.Now
              },          
             new Setting
             {
                 Id = 3,
                 Key = "Email",
                 Value = "info@example.com",
                 SoftDeleted = false,
                 CreatedDate = DateTime.Now
             });

        }

    }
}
