namespace MiniProject_eLearning_ASPNET_MVC.Models
{
    public class SocialMedia
    {
        public int Id { get; set; }
        public string SocialName { get; set; } 
        public string SocialLink { get; set; }        
        public List<InstructorSocialMedia> Instructors { get; set; }
    }
}
