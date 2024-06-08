using MiniProject_eLearning_ASPNET_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;

namespace MiniProject_eLearning_ASPNET_MVC.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISliderService _sliderService;

        public SliderViewComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliderDatas = new SliderVMVC
            {
                Sliders = await _sliderService.GetAllAsync(),
               

            };

            return await Task.FromResult(View(sliderDatas));
        }
    }

    public class SliderVMVC
    {
        public IEnumerable<Slider> Sliders { get; set; }      
    }
}
