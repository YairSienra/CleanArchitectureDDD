using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(x => x.Nombre).NotNull().WithMessage("{Nombre} no puede estar en blanco");
            RuleFor(x => x.Apellido).NotNull().WithMessage("{Apellido} no puede estar en blanco");
        }
    }
}
