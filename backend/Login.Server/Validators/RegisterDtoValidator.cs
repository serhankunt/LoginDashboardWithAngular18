using FluentValidation;
using Login.Server.DTOs;

namespace Login.Server.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .MinimumLength(3)
            .WithMessage("Ad alanı minimum 3 karakter olmalıdır.");

    }
}
