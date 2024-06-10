using Microsoft.AspNetCore.Mvc;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;

namespace MiniProject_eLearning_ASPNET_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
        private readonly ISocialMediaService _socialMediaService;

        public SocialMediaController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        public async Task<IActionResult> Index()
        {
            var socialMedias = await _socialMediaService.GetAllAsync();
            return View(socialMedias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialMedia socialMedia)
        {
            if (ModelState.IsValid)
            {
                await _socialMediaService.CreateAsync(socialMedia);
                return RedirectToAction(nameof(Index));
            }
            return View(socialMedia);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var socialMedia = await _socialMediaService.GetByIdAsync(id);
            if (socialMedia == null)
            {
                return NotFound();
            }
            return View(socialMedia);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var socialMedia = await _socialMediaService.GetByIdAsync(id);
            if (socialMedia == null)
            {
                return NotFound();
            }
            return View(socialMedia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SocialMedia socialMedia)
        {
            if (id != socialMedia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _socialMediaService.EditAsync(id, socialMedia);
                return RedirectToAction(nameof(Index));
            }
            return View(socialMedia);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _socialMediaService.GetByIdAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }
    }
}
