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
        private readonly IInstructorService _instructorService;
        public CourseController(ICourseService courseService,
                                ICategoryService categoryService,
                                IWebHostEnvironment env,
                                AppDbContext context,
                                IInstructorService instructorService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _env = env;
            _context = context;
            _instructorService = instructorService;
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
            Course course = await _courseService.GetByIdWithAllDatasAsync((int)id);

            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
    {
        ViewBag.categories = await _categoryService.GetAllSelectedAsync();
            ViewBag.instructor = await _instructorService.GetAllSelectedAsync();
            return View();
    }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM request)
    {
        ViewBag.categories = await _categoryService.GetAllSelectedAsync();
            ViewBag.instructor = await _instructorService.GetAllSelectedAsync();
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

            ViewBag.categories = await _categoryService.GetAllSelectedAsync();

            List<CourseImageVM> images = new();
            foreach (var item in course.CourseImages)
            {
                images.Add(new CourseImageVM
                {
                    Id = item.Id,
                    Image = item.Name,
                    IsMain = item.IsMain
                });
            }
            CourseEditVM response = new()
            {
                Name = course.Name,
                Duration = course.Duration,
                Rating = (int?)course.Rating,
                Price = course.Price,
                Images = images,
                CategoryId = course.CategoryId
            };

            return View(response);
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

            List<CourseImageVM> images = new();

            foreach (var item in course.CourseImages)
            {
                images.Add(new CourseImageVM
                {
                    Id = item.Id,
                    Image = item.Name,
                    IsMain = item.IsMain
                });
            }
            request.Images = images;


            if (!ModelState.IsValid)
            {
                ViewBag.categories = await _categoryService.GetAllSelectedAsync();
                return View(request);
            }


            if (await _courseService.ExistExceptByIdAsync((int)id, request.Name))
            {
                ModelState.AddModelError("Name", "This name already exist");

                return View(request);

            }


            if (request.NewImage is not null)
            {

                foreach (var item in request.NewImage)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewImages", "Input can accept only image format");
                        return View(request);

                    }
                    if (!item.CheckFileSize(500))
                    {
                        ModelState.AddModelError("NewImages", "Image size must be max 500 KB ");
                        return View(request);
                    }


                }
                foreach (var item in request.NewImage)
                {
                    string oldPath = _env.GenerateFilePath("img", item.Name);
                    oldPath.DeleteFileFromLocal();
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;
                    string newPath = _env.GenerateFilePath("img", fileName);

                    await item.SaveFileToLocalAsync(newPath);

                    course.CourseImages.Add(new CourseImage { Name = fileName });

                }

            }
            course.Name = request.Name;
            course.Duration = request.Duration;
            course.Duration = request.Rating.ToString();
            course.CategoryId = request.CategoryId;
            course.Price = request.Price;


            await _courseService.EditAsync();
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


