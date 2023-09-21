using FluentValidation;
using JamOrder.Models;

namespace JamOrder.Validators
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor(user => user.Username).NotNull().NotEmpty().WithMessage("{PropertyName} is {PropertyValue}");
            RuleFor(user => user.Name).NotNull().NotEmpty().WithMessage("{PropertyName} is {PropertyValue}");
            RuleFor(user => user.Surname).NotNull().NotEmpty().WithMessage("{PropertyName} is {PropertyValue}");
            RuleFor(user => user.Password).NotNull().NotEmpty().Length(8).WithMessage("{PropertyName} is invalid. Should be at least 8 characters and not empty");
        }
    }
}
