namespace MiniProject_eLearning_ASPNET_MVC.Services.Interfaces
{
    public interface ISettingService
    {       
        Task<Dictionary<string, string>> GetAllAsync();

    }
}
