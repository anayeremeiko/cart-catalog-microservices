using Catalog.Core.Entities;
using FluentValidation;

namespace Catalog.Core.Validators
{
	public class ItemValidator : AbstractValidator<Item>
	{
		public ItemValidator()
		{
			RuleFor(i => i.Id).NotEmpty();
			RuleFor(i => i.Name).NotEmpty().Length(1, 50).Matches(@"^[\w\s]*$");
			RuleFor(i => i.ImageUrl).Must(CustomRules.BeAValidUrl).WithMessage("Image link should have a valid URL");
			RuleFor(i => i.Category).NotNull();
			RuleFor(i => i.Price).NotNull().GreaterThan(0);
			RuleFor(i => i.Amount).NotNull().GreaterThan(0);
		}
	}
}
