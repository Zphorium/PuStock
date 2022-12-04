using DemoApplication.ViewModels.Admin.Navbar;
using FluentValidation;

namespace DemoApplication.Validators.Admin.Navbar.Add
{
    public class AddViewModelValidator : AbstractValidator<AddViewModel>
    {
        public AddViewModelValidator()
        {
            RuleFor(avm => avm.Name)
                .NotNull()
                .WithMessage("Title can't be empty")
                .NotEmpty()
                .WithMessage("Title can't be empty")
                .MinimumLength(10)
                .WithMessage("Minimum length should be 10")
                .MaximumLength(45)
                .WithMessage("Maximum length should be 45");
        }
    }
}
