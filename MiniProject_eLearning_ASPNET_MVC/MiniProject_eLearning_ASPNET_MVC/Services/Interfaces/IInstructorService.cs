﻿using Microsoft.AspNetCore.Mvc.Rendering;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Instructors;

namespace MiniProject_eLearning_ASPNET_MVC.Services.Interfaces
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>> GetAllAsync();

        Task<Instructor> GetByIdAsync(int id);

        Task CreateAsync(Instructor instructor);

        Task EditAsync(int id, Instructor instructor);

        Task DeleteAsync(Instructor instructor);

        Task<bool> ExistAsync(string name);

        Task<bool> ExistExceptByIdAsync(int id, string name);
        Task<Instructor> GetByIdWithSocialAsync(int id);
        Task<SelectList> GetAllSelectedAsync();
    }
}
