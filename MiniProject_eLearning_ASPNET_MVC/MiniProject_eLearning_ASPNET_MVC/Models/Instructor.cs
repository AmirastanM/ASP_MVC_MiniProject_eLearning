namespace MiniProject_eLearning_ASPNET_MVC.Models
{
    public class Instructor :BaseEntity
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Position { get; set; }
        public List<InstructorSocialMedia> SocialMedias { get; set; }
    }
}
