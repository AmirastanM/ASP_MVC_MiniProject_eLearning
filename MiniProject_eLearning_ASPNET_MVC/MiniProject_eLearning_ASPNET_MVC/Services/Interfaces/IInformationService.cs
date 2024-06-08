using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Informations;

namespace MiniProject_eLearning_ASPNET_MVC.Services.Interfaces
{
    public interface IInformationService
    {
        Task<IEnumerable<InformationVM>> GetAllAsync();
        Task<bool> ExistExceptByIdAsync(int id, string titel);
        Task<Information> GetByIdAsync(int id);
        Task CreateAsync(Information information);
        Task DeleteAsync(Information information);
    }
}
