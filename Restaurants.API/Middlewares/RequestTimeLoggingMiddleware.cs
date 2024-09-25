
using System.Diagnostics;

namespace Restaurants.API.Middlewares
{
    public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> _logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();
            await next(context);
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds/1000 > 4)
            {
                _logger.LogWarning("Request [{Verb}] with path {Path} tool {Time} seconds", context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds/1000);
            }
        }
    }
}
