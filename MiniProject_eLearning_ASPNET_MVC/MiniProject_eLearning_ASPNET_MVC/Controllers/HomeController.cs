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

        public HomeController(ISliderService sliderService,
                              IInformationService informationService,
                              IAboutCompanyService aboutCompanyService)
        {
            _sliderService = sliderService;
            _informationService = informationService;
            _aboutCompanyService = aboutCompanyService;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Informations = await _informationService.GetAllAsync(),
                AboutCompany = await _aboutCompanyService.GetAllAsync()
            };

            return View(model);
        }
    }
}

