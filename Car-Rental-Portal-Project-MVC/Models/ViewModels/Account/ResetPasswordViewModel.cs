using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Portal_Project_MVC.Models.ViewModels.Account
{
	public class ResetPasswordViewModel
	{
		public Guid UserId { get; set; }
		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Must be same as password")]
		public string ConfirmedPassword { get; set; }
		[Required]
		public string Code { get; set; }
	}
}
