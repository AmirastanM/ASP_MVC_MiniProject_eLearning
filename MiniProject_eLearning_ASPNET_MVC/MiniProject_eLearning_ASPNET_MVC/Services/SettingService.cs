using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MiniProject_eLearning_ASPNET_MVC.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            return await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);
        }
    }
}
