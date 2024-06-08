
using System.ComponentModel.DataAnnotations;

namespace MiniProject_eLearning_ASPNET_MVC.ViewModels.Sliders
{
    public class SliderCreateVM
    {        
        public List<IFormFile> Images { get; set; }       
        public string Heading { get; set; }      
        public string Title { get; set; }       
        public string Description { get; set; }
    }
}
