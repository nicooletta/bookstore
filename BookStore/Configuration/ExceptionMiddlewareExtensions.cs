using BookStore.Business.Exceptions;
using BookStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace BookStore.Configuration
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    //parameters for any exception
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    string errorMessage = "Internal Server Error.";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        //handle specific exception type
                        if (contextFeature.Error is ValidationFailException)
                        {
                            var validationError = (ValidationFailException)contextFeature.Error;
                            errorMessage = $"Validation error: {string.Join(", ", validationError.ValidationErrors)}";
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        else if (contextFeature.Error is ResourceNotFoundException)
                        {
                            errorMessage = $"Resource not found: {contextFeature.Error.Message}";
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        }

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = errorMessage
                        }.ToString());
                    }
                });
            });
        }
    }
}
