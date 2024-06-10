using Microsoft.AspNetCore.Mvc;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;

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
        public async Task<IActionResult> Create(SocialMedia request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool existSocial = await _socialMediaService.ExistAsync(request.SocialName);
            if (existSocial)
            {
                ModelState.AddModelError("Name", "This name already exist");
                return View();
            }
            await _socialMediaService.CreateAsync(new SocialMedia { SocialName = request.SocialName });
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int? id, SocialMedia request)
        {
            if (id == null) return BadRequest();
            var social = await _socialMediaService.GetByIdAsync((int)id);
            if (social == null) return NotFound();


            if (!ModelState.IsValid)
            {
                return View();
            }


            if (request.SocialName is not null)
            {
                social.SocialName = request.SocialName;
            }
            return RedirectToAction(nameof(Index));
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
