using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.SocialMedias;

namespace MiniProject_eLearning_ASPNET_MVC.ViewModels.Instructors
{
    public class InstructorVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Position { get; set; }
        public List<InstructorSocialMedia> instructorSocialMedias { get; set; }
    }
}
