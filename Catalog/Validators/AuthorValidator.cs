using Catalog.DTOs;
using FluentValidation;

namespace Catalog.Validators
{
	public class AuthorValidator : AbstractValidator<AuthorDTO>
	{
		public AuthorValidator()
		{
			RuleFor(author => author.Name).NotEmpty().MaximumLength(100);
			RuleFor(author => author.Bio).MaximumLength(500);
			RuleFor(author => author.BirthDate).NotEmpty();
		}
	}
}
