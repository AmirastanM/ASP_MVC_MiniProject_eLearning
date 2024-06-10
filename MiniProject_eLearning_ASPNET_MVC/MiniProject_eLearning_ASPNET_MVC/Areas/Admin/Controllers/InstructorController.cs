using Microsoft.AspNetCore.Mvc;
using MiniProject_eLearning_ASPNET_MVC.Helpers.Extentions;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Instructors;

namespace MiniProject_eLearning_ASPNET_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorController : Controller
    {

        private readonly IInstructorService _instructorService;
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;

        public InstructorController(IInstructorService instructorService, ICourseService courseService, ICategoryService categoryService, IWebHostEnvironment env)
        {
            _instructorService = instructorService;
            _courseService = courseService;
            _categoryService = categoryService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var instructors = await _instructorService.GetAllAsync();
            return View(instructors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(İnstructorCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool existInstructor = await _instructorService.ExistAsync(request.Name);
            if (existInstructor)
            {
                ModelState.AddModelError("Name", "This Instructor already exist");
                return View();
            }


            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Input can accept only image format");
                return View();
            }
            if (!request.Image.CheckFileSize(200))
            {
                ModelState.AddModelError("Image", "Image size must be max 200 KB ");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = Path.Combine(_env.WebRootPath, "img", fileName);
            await request.Image.SaveFileToLocalAsync(path);



            await _instructorService.CreateAsync(new Instructor { Name = request.Name, Image = fileName});
            return RedirectToAction(nameof(Index));
        }

       

        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _instructorService.EditAsync(id, instructor);
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null) return BadRequest();
            var instructor = await _instructorService.GetByIdAsync((int)id);
            if (instructor is null) return NotFound();
            string path = _env.GenerateFilePath("img", instructor.Image);
            path.DeleteFileFromLocal();
            await _instructorService.DeleteAsync(instructor);
            return RedirectToAction(nameof(Index));
        }
    }
}
