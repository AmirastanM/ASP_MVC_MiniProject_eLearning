using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Courses;

namespace MiniProject_eLearning_ASPNET_MVC.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllWithImagesAsync();
        Task<List<Course>> GetAllAsync();
        Task<Course> GetByIdWithAllDatasAsync(int id);
        Task<Course> GetByIdAsync(int id);      
        IEnumerable<CourseVM> GetMappedDatas(IEnumerable<Course> products);
        Task<int> GetCountAsync();
        Task CreateAsync(Course course);
        Task DeleteAsync(Course course);
        Task EditAsync();
        Task<CourseImage> GetCourseImageByIdAsync(int id);
        Task<Course> GetCourseByNameAsync(string name);
        Task ImageDeleteAsync(CourseImage image);
        Task<bool> ExistExceptByIdAsync(int id, string name);
    }
}
