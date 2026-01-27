namespace CiaAerea.Middlewares
{
    using FluentValidation;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ValidationExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.Conflict;

                var result = JsonSerializer.Serialize(new
                {
                    erros = ex.Errors.Select(erro => erro.ErrorMessage)
                });
                await response.WriteAsync(result);
            }
        }
    }
}