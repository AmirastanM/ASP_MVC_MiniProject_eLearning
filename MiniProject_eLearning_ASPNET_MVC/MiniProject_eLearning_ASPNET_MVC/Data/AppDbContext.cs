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
        public DbSet<AboutCompany> AboutCompanies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<InstructorSocialMedia> InstructorSocialMedias { get; set; }
  
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Instructor>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<SocialMedia>().HasQueryFilter(m => !m.SoftDeleted);

            modelBuilder.Entity<AboutCompany>().HasData(
           new AboutCompany
           {
               Id = 1,
               Titel = "Welcome to eLEARNING",
               Description = "Tempor erat elitr rebum at clita. Diam dolor diam ipsum sit. Aliqu diam amet diam et eos. Clita erat ipsum et lorem et sit. " +
               "" +
               "Tempor erat elitr rebum at clita. Diam dolor diam ipsum sit. Aliqu diam amet diam et eos. Clita erat ipsum et lorem et sit, sed stet lorem sit clita duo justo magna dolore erat amet",
               Image = "about.jpg"
           });


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
