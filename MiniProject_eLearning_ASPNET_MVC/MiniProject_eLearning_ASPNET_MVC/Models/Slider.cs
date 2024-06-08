using System.ComponentModel.DataAnnotations;

namespace MiniProject_eLearning_ASPNET_MVC.Models
{
    public class Slider : BaseEntity
    {    
        public string Image { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}


