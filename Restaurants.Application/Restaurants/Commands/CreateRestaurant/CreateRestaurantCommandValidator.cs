using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantDto>
    {
        List<string> ValidCategories = ["Indian", "Mexican", "American", "Australian", "African", "Italian"];
        public CreateRestaurantCommandValidator()
        {
            RuleFor(createRestarantDto => createRestarantDto.Name)
                .Length(3, 100);

            RuleFor(createRestarantDto => createRestarantDto.Category)
                .Must(ValidCategories.Contains)
                .WithMessage("Insert a valid category");
            //.Custom((value, context) =>
            //{
            //var isValidCategory = ValidCategories.Contains(value);
            //    if (!isValidCategory)
            //    {
            //        context.AddFailure("Category", "Invalid Category. Please choose valid category");
            //    }
            //});

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .WithMessage("Please enter a valid email address");

            RuleFor(dto => dto.ContactNumber)
                .Matches(@"^\d{10}$")
                .WithMessage("Please enter a valid phone number");

            RuleFor(dto => dto.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Please enter a valid postal code (XX-XXX) ");
        }

    }
}
