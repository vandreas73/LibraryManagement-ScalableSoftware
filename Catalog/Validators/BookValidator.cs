using Catalog.DTOs;
using Catalog.Models;
using FluentValidation;

namespace Catalog.Validators
{
	public class BookValidator : AbstractValidator<BookDTO>
	{
		public BookValidator()
		{
			RuleFor(book => book.Title).NotEmpty();
			RuleFor(book => book.AuthorId).NotEmpty();
			RuleFor(book => book.PublishingYear).NotEmpty();
		}
	}
}
