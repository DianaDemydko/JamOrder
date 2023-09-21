using FluentValidation;
using JamOrder.Models;

namespace JamOrder.Validators
{
    public class UserCredentialsValidator: AbstractValidator<UserCredentials>
    {
        public UserCredentialsValidator() 
        {
            RuleFor(user => user.Username).NotNull().NotEmpty().WithMessage("{PropertyName} is {PropertyValue}");
            RuleFor(user => user.Password).NotNull().NotEmpty().WithMessage("{PropertyName} is {PropertyValue}");
        }
    }
}
