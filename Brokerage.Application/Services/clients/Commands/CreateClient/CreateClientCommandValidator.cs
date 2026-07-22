using FluentValidation;

public class CreateClientCommandValidator
    : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.NationalID)
            .NotEmpty()
            .Length(14);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(15);

        RuleFor(x => x.Deposit)
            .GreaterThanOrEqualTo(0);
    }
}