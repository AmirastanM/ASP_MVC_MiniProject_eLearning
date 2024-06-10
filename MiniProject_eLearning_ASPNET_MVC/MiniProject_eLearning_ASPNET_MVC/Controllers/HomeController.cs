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
        private readonly ICourseService _courseService;
        private readonly IInstructorService _instructorService;

        public HomeController(ISliderService sliderService,
                              IInformationService informationService,
                              IAboutCompanyService aboutCompanyService,
                              ICategoryService categoryService,
                              ICourseService courseService,
                              IInstructorService instructorService)
        {
            _sliderService = sliderService;
            _informationService = informationService;
            _aboutCompanyService = aboutCompanyService;
            _categoryService = categoryService;
            _courseService = courseService;
            _instructorService = instructorService;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Informations = await _informationService.GetAllAsync(),
                AboutCompany = await _aboutCompanyService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Courses = await _courseService.GetAllAsync(),
                Instructors = await _instructorService.GetAllAsync(),
            };

            return View(model);
        }
    }
}

