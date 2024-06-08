using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace MiniProject_eLearning_ASPNET_MVC.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;

        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Slider slider)
        {
            await _context.AddAsync(slider);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Slider slider)
        {
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistExceptByIdAsync(int id, string titel)
        {
            return await _context.Sliders.AnyAsync(m => m.Title == titel && m.Id != id);
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _context.Sliders.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
