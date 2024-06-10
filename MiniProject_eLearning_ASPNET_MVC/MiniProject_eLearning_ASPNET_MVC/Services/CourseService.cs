using Microsoft.EntityFrameworkCore;
using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Courses;

namespace MiniProject_eLearning_ASPNET_MVC.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        public CourseService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.Include(m => m.CourseImages).Include(m => m.Category).ToListAsync();

        }

        public async Task<IEnumerable<Course>> GetAllWithImagesAsync()
        {
            return await _context.Courses.Include(m => m.CourseImages).ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<Course> GetByIdWithAllDatasAsync(int id)
        {
            return await _context.Courses.Where(m => m.Id == id)
                .Include(m => m.Category)
                .Include(m => m.CourseImages)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Courses.CountAsync();
        }

        public async Task<Course> GetCourseByNameAsync(string name)
        {
            return await _context.Courses.FirstOrDefaultAsync(m => m.Name == name);

        }

        public async Task<CourseImage> GetCourseImageByIdAsync(int id)
        {
            return await _context.CourseImages.FirstOrDefaultAsync(m => m.Id == id);

        }

        public IEnumerable<CourseVM> GetMappedDatas(IEnumerable<Course> courses)
        {
            return courses.Select(m => new CourseVM()
            {
                Id = m.Id,
                Name = m.Name,
                CategoryName = m.Category.Name,
                Price = m.Price,
                Duration = m.Duration,
                MainImage = m.CourseImages.FirstOrDefault(m => m.IsMain).Name
            });
        }

        public async Task ImageDeleteAsync(CourseImage image)
        {
            _context.CourseImages.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
