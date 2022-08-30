using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommnadValidator : AbstractValidator<CreateStreamerCommand>
    {
        public CreateStreamerCommnadValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{Name} no puede estar en blanco").NotNull().MaximumLength(50).WithMessage("{Nombre} no puede superar los 50 caracteres");
            RuleFor(x => x.Url).NotEmpty().WithMessage("La {url} no puede estar en blanco");
        }
    }
}
