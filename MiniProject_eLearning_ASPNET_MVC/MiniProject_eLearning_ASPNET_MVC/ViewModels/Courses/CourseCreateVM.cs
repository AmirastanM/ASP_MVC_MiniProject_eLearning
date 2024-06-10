namespace MiniProject_eLearning_ASPNET_MVC.ViewModels.Courses
{
    public class CourseCreateVM
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public double Rating { get; set; }
        public string Duration { get; set; }
        public int NumberOfStudents { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
