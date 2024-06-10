using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Categories;

namespace MiniProject_eLearning_ASPNET_MVC.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(Category category);


        Task DeleteAsync(Category category);

        Task<bool> ExistAsync(string name);

        Task<bool> ExistExceptByIdAsync(int id, string name);

        Task<IEnumerable<CategoryArchiveVM>> GetAllArchiveAsync();

        Task<IEnumerable<Category>> GetAllAsync();

        Task<SelectList> GetAllSelectedAsync();

        Task<IEnumerable<CategoryCourseVM>> GetAllWithCourseAsync();
        Task<Category> GetByIdAsync(int id);

        Task<int> GetCountAsync();

        Task<int> GetCountForArchiveAsync();

        IEnumerable<CategoryCourseVM> GetMappedDatas(IEnumerable<Category> category);
    }
}
