using System.Reflection.Metadata.Ecma335;

namespace MiniProject_eLearning_ASPNET_MVC.Models
{
    public class InstructorSocialMedia
    {
        public int Id { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        public int SocialMediaId { get; set; }
        public SocialMedia SocialMedia { get; set; }
        public string SocialMediaLink  { get; set; }
    }
}
