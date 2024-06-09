namespace MiniProject_eLearning_ASPNET_MVC.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<Course> Courses { get; set; }

    }
}
