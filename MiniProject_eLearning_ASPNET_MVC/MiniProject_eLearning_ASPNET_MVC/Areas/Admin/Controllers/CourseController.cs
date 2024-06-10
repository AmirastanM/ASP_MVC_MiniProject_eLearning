using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Helpers.Extentions;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Categories;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Courses;

namespace MiniProject_eLearning_ASPNET_MVC.Areas.Admin.Controllers
{

    [Area("admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        public CourseController(ICourseService courseService,
                                ICategoryService categoryService,
                                IWebHostEnvironment env,
                                AppDbContext context)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _env = env;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllAsync();

            var courseVMs = courses.Select(course => new CourseVM
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                Rating = course.Rating,
                Duration = course.Duration,
                NumberOfStudents = course.NumberOfStudents,               
                CategoryName = course.Category.Name,               
                InstructorName = course.Instructor.Name,
                MainImage = course.CourseImages.FirstOrDefault(img => img.IsMain)?.Name
            }).ToList();

            return View(courseVMs);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var existProduct = await _courseService.GetByIdWithAllDatasAsync((int)id);
            if (existProduct is null) return NotFound();

            List<CourseImageVM> images = new();
            foreach (var item in existProduct.CourseImages)
            {
                images.Add(new CourseImageVM
                {
                    Image = item.Name,
                    IsMain = item.IsMain
                });
            }

            CourseDetailVM response = new()
            {
                Id = existProduct.Id,
                Name = existProduct.Name,
                Price = existProduct.Price,
                Rating = existProduct.Rating,
                Duration = existProduct.Duration,
                NumberOfStudents = existProduct.NumberOfStudents,
                CategoryName = existProduct.Category.Name,
                InstructorName = existProduct.Instructor.Name,
                Images = images
            };

            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
    {
        ViewBag.categories = await _categoryService.GetAllSelectedAsync();
        return View();
    }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM request)
    {
        ViewBag.categories = await _categoryService.GetAllSelectedAsync();

        if (!ModelState.IsValid)
        {
            return View();
        }

        foreach (var item in request.Images)
        {
            if (!item.CheckFileSize(500))
            {
                ModelState.AddModelError("Images", "Размер изображения должен быть не более 500 КБ");
                return View();
            }

            if (!item.CheckFileType("image/"))
            {
                ModelState.AddModelError("Images", "Тип файла должен быть только изображение");
                return View();
            }
        }

        List<CourseImage> images = new();
        foreach (var item in request.Images)
        {
            string fileName = $"{Guid.NewGuid()}-{item.FileName}";
            string path = _env.GenerateFilePath("img", fileName);
            await item.SaveFileToLocalAsync(path);
            images.Add(new CourseImage { Name = fileName });
        }

        if (images.Any())
        {
            images.First().IsMain = true;
        }

        Course course = new()
        {
            Name = request.Name,
            Price = decimal.Parse(request.Price.Replace(".", ",")),
            Rating = request.Rating,
            Duration = request.Duration,
            NumberOfStudents = request.NumberOfStudents,
            Category = await _categoryService.GetByIdAsync(request.CategoryId),
            CourseImages = images,
            InstructorId = request.InstructorId
        };

        await _courseService.CreateAsync(course);

        return RedirectToAction(nameof(Index));
    }
  
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
      {
      if (id is null) return BadRequest();
      var existCourse = await _courseService.GetByIdWithAllDatasAsync((int)id);
      if (existCourse is null) return NotFound();

        foreach (var item in existCourse.CourseImages)
             {
               string path = _env.GenerateFilePath("img", item.Name);

                 path.DeleteFileFromLocal();
             }
              await _courseService.DeleteAsync(existCourse);
              return RedirectToAction(nameof(Index));
            }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var course = await _courseService.GetByIdAsync((int)id);
            if (course is null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllAsync();

            var viewModel = new CourseVM
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,                            
                NumberOfStudents = course.NumberOfStudents,              
                CategoryName = course.Category?.Name,         
                InstructorName = course.Instructor?.Name,
                MainImage = course.CourseImages?.FirstOrDefault(ci => ci.IsMain)?.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CourseEditVM request)
        {
            if (id == null || id != request.Id)
            {
                return BadRequest();
            }

            var course = await _courseService.GetByIdAsync((int)id);
            if (course == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
               
                return View(request);
            }

          
            course.Name = request.Name;
            course.Price = request.Price;





            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> isMain(int? id)
        {
            if (id is null) return BadRequest();
            var productImage = await _courseService.GetCourseImageByIdAsync((int)id);

            if (productImage is null) return NotFound();


            var productID = productImage.CourseId;

            var pro = await _courseService.GetByIdWithAllDatasAsync(productID);
            foreach (var item in pro.CourseImages)
            {
                item.IsMain = false;
            }
            productImage.IsMain = true;



            await _courseService.EditAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> imageDelete(int? id)
        {
            if (id is null) return BadRequest();
            var courseImage = await _courseService.GetCourseImageByIdAsync((int)id);

            if (courseImage is null) return NotFound();


            string path = _env.GenerateFilePath("img", courseImage.Name);

            path.DeleteFileFromLocal();


            await _courseService.ImageDeleteAsync(courseImage);
            return RedirectToAction(nameof(Index));

        }
    }
}


