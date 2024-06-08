using MiniProject_eLearning_ASPNET_MVC.ViewModels;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MiniProject_eLearning_ASPNET_MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly ISliderService _sliderService;
        private readonly IInformationService _informationService;   

        public HomeController(ISliderService sliderService,
                              IInformationService informationService)
        {
            _sliderService = sliderService;
            _informationService = informationService;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Informations = await _informationService.GetAllAsync(),
            };

            return View(model);
        }
    }
}

