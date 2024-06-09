namespace MiniProject_eLearning_ASPNET_MVC.ViewModels.Instructors
{
    public class InstructorVM
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Position { get; set; }
        public List<SocialMediaVM> SocialMedias { get; set; }
    }
}
