using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Helpers.Extentions;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Sliders;

namespace MiniProject_eLearning_ASPNET_MVC.Areas.Admin.Controllers
{
    [Area("admin")]
  
        public class SliderController : Controller
        {
            private readonly AppDbContext _context;
            private readonly IWebHostEnvironment _env;
            private readonly ISliderService _sliderService;
        public SliderController(AppDbContext context, 
                                IWebHostEnvironment env,
                                ISliderService sliderService)
            {
                _context = context;
                _env = env;
                _sliderService = sliderService;
            }
            public async Task<IActionResult> Index()
            {
                List<SliderVM> sliders = await _context.Sliders.Select(m => new SliderVM { Id = m.Id, Image = m.Image, CreatedDate = m.CreatedDate.ToString(), Title = m.Title})
                                                     .ToListAsync();
                return View(sliders);
            }
            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(SliderCreateVM request)
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

                    // return Content(fileName);

                    string path = Path.Combine(_env.WebRootPath, "img", fileName);
                    await item.SaveFileToLocalAsync(path);
                    await _context.Sliders.AddAsync(new Models.Slider { Image = fileName, Heading = request.Heading, Title = request.Title, Description = request.Description });
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null) return BadRequest();
                var deleteSlider = await _context.Sliders.FindAsync(id);
                if (deleteSlider == null) return NotFound();
                string path = _env.GenerateFilePath("img", deleteSlider.Image);

                path.DeleteFileFromLocal();
             

                _context.Sliders.Remove(deleteSlider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            [HttpGet]
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null) return BadRequest();
                var slider = await _context.Sliders.FindAsync(id);
                if (slider == null) return NotFound();
                return View(new SliderEditVM { Image = slider.Image, Heading = slider.Heading, Title = slider.Title, Description = slider.Description});
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int? id, SliderEditVM request)
            {
            if (!ModelState.IsValid)
                 {
                     return View();
                  }
            if (id == null) return BadRequest();
                var slider = await _context.Sliders.FindAsync(id);
                if (slider == null) return NotFound();
                if (request.NewImage is null) return RedirectToAction(nameof(Index));
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "Input can accept only image format");
                    request.Image = slider.Image;
                    return View(request);
                }
                if (!request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size must be max 200 KB ");
                    request.Image = slider.Image;
                    return View(request);
                }
                if(await _sliderService.ExistExceptByIdAsync((int)id, request.Title))
                {
                ModelState.AddModelError("Titel", "This titel name already exist");
                }
                if(slider.Title == request.Title)
                {
                    return RedirectToAction(nameof(Index));
                }
                           

                
                string oldPath = _env.GenerateFilePath("img", slider.Image);
                oldPath.DeleteFileFromLocal();
                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
                string newPath = _env.GenerateFilePath("img", fileName);
                await request.NewImage.SaveFileToLocalAsync(newPath);
                slider.Image = fileName;
                slider.Title = request.Title;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            [HttpGet]
            public async Task<IActionResult> Detail(int? id)
        {
            Slider slider = await _sliderService.GetByIdAsync((int)id);
            return View(slider);
        }

    }
    }
