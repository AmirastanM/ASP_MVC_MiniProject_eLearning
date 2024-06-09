namespace MiniProject_eLearning_ASPNET_MVC.Models
{
    public class Student : BaseEntity
    { 
        public string Name { get; set; }
        public string CourseName { get; set; } 
        public string Description { get; set; }       
        public int CourseId { get; set; }
        public Course CourseInfo { get; set; } 
    }
}
