using Catalog.Core.Entities;
using FluentValidation;

namespace Catalog.Core.Validators
{
	public class CategoryValidator : AbstractValidator<Category>
	{
		public CategoryValidator()
		{
			RuleFor(c => c.Id).NotEmpty();
			RuleFor(c => c.Name).NotEmpty().Length(1, 50).Matches(@"^[\w\s]*$");
			RuleFor(c => c.ImageUrl).Must(CustomRules.BeAValidUrl).WithMessage("Image link should have a valid URL");
			RuleForEach(c => c.Items).SetValidator(new ItemValidator());
		}
	}
}
