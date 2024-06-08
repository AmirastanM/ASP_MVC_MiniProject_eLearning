using MiniProject_eLearning_ASPNET_MVC.Models;

namespace MiniProject_eLearning_ASPNET_MVC.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<bool> ExistExceptByIdAsync(int id, string titel);
        Task<Slider> GetByIdAsync(int id);
        Task CreateAsync(Slider slider);
        Task DeleteAsync(Slider slider);
    }
}
