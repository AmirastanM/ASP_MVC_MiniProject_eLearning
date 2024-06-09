using Microsoft.EntityFrameworkCore;
using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Informations;

namespace MiniProject_eLearning_ASPNET_MVC.Services
{
    public class InformationService : IInformationService
    {
        private readonly AppDbContext _context;

        public InformationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Information information)
        {
            await _context.AddAsync(information);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Information information)
        {
            _context.Informations.Remove(information);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistExceptByIdAsync(int id, string titel)
        {
            return await _context.Informations.AnyAsync(m => m.Titel == titel && m.Id != id);
        }

        public async Task<IEnumerable<Information>> GetAllAsync()
        {

            //IEnumerable<Information> informations = await _context.Informations.ToListAsync();


            //var result = informations.Select(m => new InformationVM
            //{
            //    Titel = m.Titel,
            //    Description = m.Description,
            //    Image = m.Image
            //});


            //return result;

            return await _context.Informations.ToListAsync();
        }

        public async Task<Information> GetByIdAsync(int id)
        {
            return await _context.Informations.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
        }

        
    }
}
