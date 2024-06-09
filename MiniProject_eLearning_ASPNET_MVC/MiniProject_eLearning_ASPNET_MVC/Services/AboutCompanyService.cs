using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Informations;

namespace MiniProject_eLearning_ASPNET_MVC.Services
{
    public class AboutCompanyService : IAboutCompanyService
    {
        private readonly AppDbContext _context;

        public AboutCompanyService(AppDbContext context)
        {
            _context = context;
        }
      

        public async Task<IEnumerable<AboutCompany>> GetAllAsync()
        {
            return await _context.AboutCompanies.ToListAsync();
        }

        public async Task<AboutCompany> GetByIdAsync(int id)
        {
            return await _context.AboutCompanies.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
