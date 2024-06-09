using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Informations;

namespace MiniProject_eLearning_ASPNET_MVC.ViewModels
{
    public class HomeVM
    {        
       public IEnumerable<Information> Informations { get; set; }
       
       public IEnumerable<AboutCompany> AboutCompany { get; set; }
    }
}


