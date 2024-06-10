using MiniProject_eLearning_ASPNET_MVC.ViewModels;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MiniProject_eLearning_ASPNET_MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly ISliderService _sliderService;
        private readonly IInformationService _informationService;
        private readonly IAboutCompanyService _aboutCompanyService;
        private readonly ICategoryService _categoryService;

        public HomeController(ISliderService sliderService,
                              IInformationService informationService,
                              IAboutCompanyService aboutCompanyService,
                              ICategoryService categoryService)
        {
            _sliderService = sliderService;
            _informationService = informationService;
            _aboutCompanyService = aboutCompanyService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Informations = await _informationService.GetAllAsync(),
                AboutCompany = await _aboutCompanyService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
            };

            return View(model);
        }
    }
}

