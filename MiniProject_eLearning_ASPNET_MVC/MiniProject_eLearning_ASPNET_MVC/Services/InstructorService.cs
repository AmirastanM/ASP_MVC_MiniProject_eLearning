using Microsoft.EntityFrameworkCore;
using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Courses;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Instructors;

namespace MiniProject_eLearning_ASPNET_MVC.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly AppDbContext _context;

        public InstructorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _context.Instructors.ToListAsync();
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            return await _context.Instructors.FindAsync(id);
        }

        public async Task CreateAsync(Instructor instructor)
        {
            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, Instructor instructor)
        {
            var existingInstructor = await _context.Instructors.FindAsync(id);
            if (existingInstructor != null)
            {
                existingInstructor.Name = instructor.Name;
                existingInstructor.Photo = instructor.Photo;
                existingInstructor.Position = instructor.Position;
                existingInstructor.SocialMedias = instructor.SocialMedias;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Instructors.AnyAsync(m => m.Name == name);
        }

        public async Task<bool> ExistExceptByIdAsync(int id, string name)
        {
            return await _context.Instructors.AnyAsync(m => m.Id != id && m.Name == name);
        }


        
        }
    }

