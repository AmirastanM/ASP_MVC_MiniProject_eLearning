namespace MiniProject_eLearning_ASPNET_MVC.ViewModels.Courses
{
    public class CourseVM
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public string Duration { get; set; }
        public int NumberOfStudents { get; set; }
        public string Image { get; set; }
        public int InstructorId { get; set; } 
        public string InstructorName { get; set; } 
    }
}
