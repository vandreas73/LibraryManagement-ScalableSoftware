using FluentValidation;
using UserManagementService.Models;

namespace UserManagementService.Validators
{
	public class UserValidator : AbstractValidator<UserDTO>
	{
		public UserValidator()
		{
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Address).NotEmpty();
		}
	}
}
