﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Categories;

namespace MiniProject_eLearning_ASPNET_MVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<bool> ExistExceptByIdAsync(int id, string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name == name && m.Id != id);
        }

        public async Task<IEnumerable<CategoryArchiveVM>> GetAllArchiveAsync()
        {
            IEnumerable<Category> categories = await _context.Categories.IgnoreQueryFilters()
                            .Where(m => m.SoftDeleted)
                            .OrderByDescending(m => m.Id)
                            .ToListAsync();
            return categories.Select(m => new CategoryArchiveVM
            {
                CategoryName = m.Name,
                Id = m.Id,
                CreatedDate = m.CreatedDate.ToString("dd.MM.yyyy"),
                CourseCount = m.Courses.Count(),
            });
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<SelectList> GetAllSelectedAsync()
        {
            var categories = await _context.Categories.Where(m => !m.SoftDeleted).ToListAsync();
            return new SelectList(categories, "Id", "Name");
        }

        public async Task<IEnumerable<CategoryCourseVM>> GetAllWithCourseAsync()
        {
            IEnumerable<Category> categories = await _context.Categories.Include(m => m.Courses)
                                                                       .OrderByDescending(m => m.Id)
                                                                       .ToListAsync();
            return categories.Select(m => new CategoryCourseVM
            {
                CategoryName = m.Name,
                Id = m.Id,
                CreatedDate = m.CreatedDate.ToString("dd.MM.yyyy"),
                CourseCount = m.Courses.Count
            });
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<int> GetCountForArchiveAsync()
        {
            return await _context.Categories.Where(m => m.SoftDeleted).CountAsync();
        }

        public IEnumerable<CategoryCourseVM> GetMappedDatas(IEnumerable<Category> category)
        {
            return category.Select(m => new CategoryCourseVM()
            {
                Id = m.Id,
                CreatedDate = m.CreatedDate.ToString("dddd.MM.yyyy"),
                CourseCount = m.Courses.Count,
                CategoryName = m.Name
            }); ;
        }
    }
}