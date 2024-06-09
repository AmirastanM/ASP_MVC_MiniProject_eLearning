using MiniProject_eLearning_ASPNET_MVC.ViewModels.Courses;

namespace MiniProject_eLearning_ASPNET_MVC.ViewModels.Categories
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public List<CourseVM> Courses { get; set; }
    }
}
