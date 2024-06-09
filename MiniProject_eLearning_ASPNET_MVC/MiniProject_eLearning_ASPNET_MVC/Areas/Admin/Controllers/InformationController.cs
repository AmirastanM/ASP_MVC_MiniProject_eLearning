using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Helpers.Extentions;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Informations;


namespace MiniProject_eLearning_ASPNET_MVC.Areas.Admin.Controllers
{
    [Area("admin")]
    public class InformationController : Controller
    {
        private readonly IInformationService _informationService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public InformationController(IInformationService informationService,
                                     AppDbContext context,
                                     IWebHostEnvironment env)
        {
            _informationService = informationService;
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<InformationVM> informations = await _context.Informations.Select(m => new InformationVM { Id = m.Id, Image = m.Image,Titel = m.Titel, Description = m.Description })
                                                      .ToListAsync();
            return View(informations);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InformationCreateVM request)
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
                await _context.Informations.AddAsync(new Models.Information { Image = fileName, Titel = request.Title, Description = request.Description });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var deleteInformation = await _context.Informations.FindAsync(id);
            if (deleteInformation == null) return NotFound();
            string path = _env.GenerateFilePath("img", deleteInformation.Image);

            path.DeleteFileFromLocal();


            _context.Informations.Remove(deleteInformation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            var information = await _context.Informations.FindAsync(id);
            if (information == null) return NotFound();
            return View(new InformationEditVM { Image = information.Image, Title = information.Titel, Description = information.Description });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, InformationEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null) return BadRequest();
            var information = await _context.Informations.FindAsync(id);
            if (information == null) return NotFound();
            if (request.NewImage is null) return RedirectToAction(nameof(Index));
            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Input can accept only image format");
                request.Image = information.Image;
                return View(request);
            }
            if (!request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200 KB ");
                request.Image = information.Image;
                return View(request);
            }
            if (await _informationService.ExistExceptByIdAsync((int)id, request.Title))
            {
                ModelState.AddModelError("Titel", "This titel name already exist");
            }
            if (information.Titel == request.Title)
            {
                return RedirectToAction(nameof(Index));
            }

            string oldPath = _env.GenerateFilePath("img", information.Image);
            oldPath.DeleteFileFromLocal();
            string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
            string newPath = _env.GenerateFilePath("img", fileName);
            await request.NewImage.SaveFileToLocalAsync(newPath);
            information.Image = fileName;
            information.Titel = request.Title;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            Information information = await _informationService.GetByIdAsync((int)id);
            return View(information);
        }


    }
}
