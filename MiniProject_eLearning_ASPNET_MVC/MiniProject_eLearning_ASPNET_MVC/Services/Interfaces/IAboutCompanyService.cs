using MiniProject_eLearning_ASPNET_MVC.Models;

namespace MiniProject_eLearning_ASPNET_MVC.Services.Interfaces
{
    public interface IAboutCompanyService
    {
        Task<IEnumerable<AboutCompany>> GetAllAsync();     
        Task<AboutCompany> GetByIdAsync(int id);
 
    }
}
