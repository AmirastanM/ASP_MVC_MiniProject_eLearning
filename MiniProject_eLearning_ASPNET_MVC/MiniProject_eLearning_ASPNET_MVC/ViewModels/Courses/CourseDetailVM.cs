namespace MiniProject_eLearning_ASPNET_MVC.ViewModels.Courses
{
    public class CourseDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public string Duration { get; set; }
        public int NumberOfStudents { get; set; }
    
        public string CategoryName { get; set; }
   
        public string InstructorName { get; set; }
        public List<CourseImageVM> Images { get; set; }
    }
}
