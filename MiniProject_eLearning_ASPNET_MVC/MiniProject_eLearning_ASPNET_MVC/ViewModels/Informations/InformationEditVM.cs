namespace MiniProject_eLearning_ASPNET_MVC.ViewModels.Informations
{
    public class InformationEditVM
    {
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }        
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
