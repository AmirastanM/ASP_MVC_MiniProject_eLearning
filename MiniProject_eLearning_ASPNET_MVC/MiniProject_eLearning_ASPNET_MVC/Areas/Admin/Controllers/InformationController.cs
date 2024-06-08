using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject_eLearning_ASPNET_MVC.Data;
using MiniProject_eLearning_ASPNET_MVC.Helpers.Extentions;
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
            return View(await _informationService.GetAllAsync());
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
    }
}
