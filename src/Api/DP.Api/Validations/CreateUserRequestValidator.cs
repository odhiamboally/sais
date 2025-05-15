using DP.Application.Dtos.Auth;
using FluentValidation;

namespace DP.Api.Validations;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;

        When(x => x != null, () => {

            RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Matches("^[a-zA-Z0-9]+$");


        });

    }
}
