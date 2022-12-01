﻿
using PostingManagement.UI.Exceptions;
using System.Net;
using System.Security.Authentication;

namespace PostingManagement.UI.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next; _logger = logger;
        }
        //Catch Exception globally.
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "", null);
                var message = ConvertException(context, ex);
                context.Response.Redirect("/ErrorHandler/" + message);
            }
        }
       // Throws Error with the respective Status Code.
        private int ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case NotImplementedException appexception:
                    httpStatusCode = HttpStatusCode.NotImplemented;
                    break;
                case UnauthorizedAccessException unAuthException:
                    httpStatusCode = HttpStatusCode.Unauthorized;
                    break;
                case AuthenticationException authenticationException:
                    httpStatusCode = HttpStatusCode.ServiceUnavailable;
                    break;
                case Exception ex:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    break;
            }
            return (int)httpStatusCode;
        }


    }
}