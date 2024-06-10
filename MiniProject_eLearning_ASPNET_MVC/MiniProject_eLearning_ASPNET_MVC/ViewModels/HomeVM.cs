using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Informations;
using MiniProject_eLearning_ASPNET_MVC.ViewModels.Instructors;

namespace MiniProject_eLearning_ASPNET_MVC.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<InstructorVM> Instructors { get; set; }
        public IEnumerable<Information> Informations { get; set; }
        public IEnumerable<AboutCompany> AboutCompany { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Course> Courses { get; set; }

        public HomeVM()
        {
            Instructors = new List<InstructorVM>();
            Informations = new List<Information>();
            Categories = new List<Category>();
            AboutCompany = new List<AboutCompany>();
            Courses = new List<Course>();
        }
    }

}


