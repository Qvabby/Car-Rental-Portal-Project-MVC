namespace Car_Rental_Portal_Project_MVC.Services.Interfaces
{
    public interface IEmaiService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
