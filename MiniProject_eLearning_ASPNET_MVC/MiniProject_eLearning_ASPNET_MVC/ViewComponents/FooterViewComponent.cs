using Microsoft.AspNetCore.Mvc;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;

namespace EducalBackend.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        
        private readonly ISettingService _settingService;
        public FooterViewComponent(ISettingService settingService)
        {
        
            _settingService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = new FooterVMVC
            {
           
                Settings = await _settingService.GetAllAsync(),
            };

            return View(datas);
        }
    }
    public class FooterVMVC
    {
    
        public IDictionary<string, string> Settings { get; set; }
    }
}
