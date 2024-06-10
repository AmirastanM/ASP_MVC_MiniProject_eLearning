namespace MiniProject_eLearning_ASPNET_MVC.ViewModels.Instructors
{
    public class İnstructorCreateVM
    {
        public int İd { get; set; }
        public string Name { get; set; }
        public List<IFormFile> Images { get; set; }
        public string Position { get; set; }
    }
}
