using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Exeptions
{
    public class ValidationExeption : ApplicationException
    {
      

        public ValidationExeption() : base("Se presetaron uno o mas errores de validacion")
        {
            errors = new Dictionary<string, string[]>();
        }

        public ValidationExeption(IEnumerable<ValidationFailure> failures) : this()
        {
            errors = failures
                .GroupBy(x => x.PropertyName,  m => m.ErrorMessage)
                .ToDictionary(failuresGroup => failuresGroup.Key, failuresGroup => failuresGroup.ToArray());
        }
            public IDictionary<string, string[]> errors { get; }
    }
}
