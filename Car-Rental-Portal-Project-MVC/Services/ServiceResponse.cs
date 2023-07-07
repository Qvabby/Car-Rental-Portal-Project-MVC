namespace Car_Rental_Portal_Project_MVC.Services
{
    public class ServiceResponse<T>
    {
        //This is the place where the Response Data will be stored.
        public T? Data { get; set; }
        //This is the string where Response Message will be stored.
        public string Message { get; set; } = string.Empty;
        //here you can store string that will tell in detail what the response did.
        public string Description { get; set; } = string.Empty;
        //here you can check if response was successful or not.
        public bool success { get; set; } = true;
    }
}
