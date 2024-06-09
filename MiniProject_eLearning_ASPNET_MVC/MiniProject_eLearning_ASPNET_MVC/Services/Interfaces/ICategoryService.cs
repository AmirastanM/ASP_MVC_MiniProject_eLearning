using Microsoft.AspNetCore.Mvc.Rendering;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Categories;

namespace MiniProject_eLearning_ASPNET_MVC.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<CategoryCourseVM>> GetAllWithCourseAsync();
        Task<Category> GetByIdAsync(int id);
        Task<bool> ExistAsync(string name);
        Task CreateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<bool> ExistExceptByIdAsync(int id, string name);
        Task<IEnumerable<CategoryArchiveVM>> GetAllArchiveAsync();                
        IEnumerable<CategoryCourseVM> GetMappedDatas(IEnumerable<Category> category);
        Task<int> GetCountAsync();
        Task<int> GetCountForArchiveAsync();
        Task<SelectList> GetAllSelectedAsync();
    }
}
