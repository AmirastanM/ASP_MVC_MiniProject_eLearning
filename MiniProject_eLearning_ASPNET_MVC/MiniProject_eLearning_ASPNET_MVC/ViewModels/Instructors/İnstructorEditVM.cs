namespace MiniProject_eLearning_ASPNET_MVC.ViewModels.Instructors
{
    public class İnstructorEditVM
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }        
        public string Position { get; set; }
        public IFormFile? NewImages { get; set; }
    }
}
