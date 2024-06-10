using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Helpers.Extentions;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Categories;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Informations;
using Org.BouncyCastle.Asn1.Ocsp;

namespace MiniProject_eLearning_ASPNET_MVC.Areas.Admin.Controllers
{

    [Area("admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CategoryController(ICategoryService categoryService,
                                   AppDbContext context,
                                   IWebHostEnvironment env)
        {
            _categoryService = categoryService;
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();

            List<CategoryCourseVM> categoryVM = categories.Select(category => new CategoryCourseVM
            {
                Id = category.Id,
                CategoryName = category.Name,
                CreatedDate = category.CreatedDate.ToString(),
                CourseCount = 0
            }).ToList();


            return View(categoryVM);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Input can accept only image format");
                    return View();
                }
                if (!item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Image", "Image size must be max 200 KB ");
                    return View();
                }
            }
            foreach (var item in request.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;



                string path = Path.Combine(_env.WebRootPath, "img", fileName);
                await item.SaveFileToLocalAsync(path);
                await _context.Categories.AddAsync(new Category { Image = fileName, Name = item.Name});
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var category = await _categoryService.GetByIdAsync((int)id);
            if (category is null) return NotFound();
            await _categoryService.DeleteAsync(category);
            if (category.SoftDeleted)
                return RedirectToAction("CategoryArchive", "Archive");


            return RedirectToAction(nameof(Index));



        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            Category category = await _context.Categories.Include(m => m.Courses).ThenInclude(m => m.CourseImages).FirstOrDefaultAsync(m => m.Id == id);


            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var category = await _categoryService.GetByIdAsync((int)id);
            if (category is null) return NotFound();
            return View(new CategoryEditVM { Name = category.Name });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();

            if (await _categoryService.ExistExceptByIdAsync((int)id, request.Name))
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }

            var category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            if (category.Name == request.Name)
            {
                return RedirectToAction(nameof(Index));
            }



            category.Name = request.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> SetToArchive(int? id)
        {
            if (id is null) return BadRequest();
            var category = await _categoryService.GetByIdAsync((int)id);
            if (category is null) return NotFound();
            category.SoftDeleted = !category.SoftDeleted;
            await _context.SaveChangesAsync();

            return Ok(category);
        }
    }
}
       

