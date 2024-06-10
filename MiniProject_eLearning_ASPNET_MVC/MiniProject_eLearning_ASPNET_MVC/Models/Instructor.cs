namespace MiniProject_eLearning_ASPNET_MVC.Models
{
    public class Instructor :BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Position { get; set; }
        public ICollection<InstructorSocialMedia> SocialMedias { get; set; }
    }
}
