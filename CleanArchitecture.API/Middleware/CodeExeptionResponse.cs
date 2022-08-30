namespace CleanArchitecture.API.Middleware
{
    public class CodeExeptionResponse
    {
        public int StatusCodes { get; set; }
        public string? Message { get; set; } 

        public CodeExeptionResponse(int statusCodes, string? message = null)
        {
            StatusCodes = statusCodes;
            Message = message ?? GetDefaultMessegeStatusCode(statusCodes);
        }


        private string GetDefaultMessegeStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "el request enviado tiene errores",
                401 => "No tiene autorizacion para este recurso",
                404 => "No se encontro el recuso solicitado",
                500 => "Se produjeron errores en el servidor",
                _ => string.Empty,
            };
        }
    }
}
