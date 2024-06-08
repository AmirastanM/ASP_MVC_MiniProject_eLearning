﻿

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;

namespace MiniProject_eLearning_ASPNET_MVC.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;


        public HeaderViewComponent(ISettingService settingService,
                                   IHttpContextAccessor accessor,
                                   UserManager<AppUser> userManager)
        {
            _settingService = settingService;
            _accessor = accessor;
            _userManager = userManager;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser user = new();
            if(User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }          

            Dictionary<string, string> settings = await _settingService.GetAllAsync();

            var response = new HeaderVM
            {
                Settings = settings,
                UserFullName = user.FullName,
            
            };

            return await Task.FromResult(View(response));
        }
    }

    public class HeaderVM
    {

        public Dictionary<string, string> Settings { get; set; }
        public string UserFullName { get; set; }

    }

}