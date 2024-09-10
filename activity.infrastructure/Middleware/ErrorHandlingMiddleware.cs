using activity.infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activity.infrastructure.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException exception)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(exception.Message);
            }
            catch(UnauthorizedException exception)
            {
                context.Response.StatusCode =401 ;
                await context.Response.WriteAsync(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Coś poszło nie tak");
                
            }
        }
    }
}
