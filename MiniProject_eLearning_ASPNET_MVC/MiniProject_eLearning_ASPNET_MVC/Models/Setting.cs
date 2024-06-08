using MiniProject_eLearning_ASPNET_MVC.Models;

namespace EducalBackend.Models
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
