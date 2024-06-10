using Microsoft.AspNetCore.Mvc;
using MiniProject_eLearning_ASPNET_MVC.Models;

namespace MiniProject_eLearning_ASPNET_MVC.Services.Interfaces
{
    public interface ISocialMediaService
    {
        Task<SocialMedia> GetByIdAsync(int id);
        Task CreateAsync(SocialMedia socialMedia);
        Task DeleteAsync(SocialMedia socialMedia);
        Task<bool> ExistAsync(string socialName);
        Task<IEnumerable<SocialMedia>> GetAllByInstructorIdAsync(int instructorId);

        Task EditAsync(int id, SocialMedia socialMedia);
        Task<IEnumerable<SocialMedia>> GetAllAsync();
    }
}
