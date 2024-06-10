using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;

namespace MiniProject_eLearning_ASPNET_MVC.Services
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly AppDbContext _context;

        public SocialMediaService(AppDbContext context)
        {
            _context = context;
        }


        public async Task CreateAsync(SocialMedia socialMedia)
        {
            _context.SocialMedias.Add(socialMedia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SocialMedia socialMedia)
        {
            _context.SocialMedias.Remove(socialMedia);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string socialName)
        {
            return await _context.SocialMedias.AnyAsync(s => s.SocialName == socialName);
        }

        public async Task<IEnumerable<SocialMedia>> GetAllAsync()
        {
            return await _context.SocialMedias.ToListAsync();
        }

        public async Task<SelectList> GetAllSelectedAsync()
        {
            var socials = await _context.SocialMedias.Where(m => !m.SoftDeleted).ToListAsync();
            return new SelectList(socials, "Id", "Name");

        }

        public async Task<SocialMedia> GetByIdAsync(int id)
        {
            return await _context.SocialMedias.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
