using Microsoft.AspNetCore.Mvc.Rendering;
using MiniProject_eLearning_ASPNET_MVC.Models;

namespace MiniProject_eLearning_ASPNET_MVC.ViewModels.Instructors
{
    public class InstructorCourseVM
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Position { get; set; }
        public ICollection<Course> Courses { get; set; }
        public SelectList CourseList { get; set; }
    }
}
