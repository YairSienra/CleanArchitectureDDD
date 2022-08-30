using CleanArchitecture.API.Middleware;

namespace CleanArchitecture.API.Errors
{
    public class CodeErrorExeption : CodeExeptionResponse
    {   
        public string? Details { get; set; }
        public CodeErrorExeption(int statusCodes, string? message = null, string? details = null) : base(statusCodes, message)
        {
            Details = details;
        }
    }
}
