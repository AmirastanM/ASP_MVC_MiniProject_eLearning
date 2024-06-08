using Microsoft.AspNetCore.Identity;

namespace MiniProject_eLearning_ASPNET_MVC.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
