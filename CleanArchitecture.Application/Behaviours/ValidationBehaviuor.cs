using FluentValidation;
using MediatR;
using ValidationExeption = CleanArchitecture.Application.Exeptions.ValidationExeption;
namespace CleanArchitecture.Application.Behaviours
{
    public class ValidationBehaviuor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        public readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviuor(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if(_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validatorsResults = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));

                var failuresErrors =  validatorsResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failuresErrors.Count != 0)
                {
                    throw new ValidationException(failuresErrors);
                } 
            }
            return await next();
        }
    }
}
