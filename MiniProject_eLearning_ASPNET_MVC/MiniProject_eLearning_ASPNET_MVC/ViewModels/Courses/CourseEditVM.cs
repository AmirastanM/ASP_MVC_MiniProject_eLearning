namespace MiniProject_eLearning_ASPNET_MVC.ViewModels.Courses
{
    public class CourseEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? Rating { get; set; }
        public string Duration { get; set; }
        public int NumberOfStudents { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string InstructorName { get; set; }
        public List<CourseImageVM> Images { get; set; }
        public List<IFormFile> NewImage { get; set; }
    }
}