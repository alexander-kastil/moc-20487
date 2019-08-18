using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlueYonderHotels.Service.Middleware
{
    public static class ExecutionTimeMiddleware
    {
        public static IApplicationBuilder UseExecutionTimeMiddleware(this IApplicationBuilder app)
        {
            app.Use(AddResponeHeaders);
            return app;
        }

        private static async Task AddResponeHeaders(HttpContext context,Func<Task> next)
        {
            context.Response.Headers.Add("X-Server-Name", Environment.MachineName);
            context.Response.Headers.Add("X-OS-Version", Environment.OSVersion.VersionString);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            context.Response.OnStarting(state => {
                var httpContext = (HttpContext)state;
                stopwatch.Stop();
                httpContext.Response.Headers.Add("X-Request-Execution-Time", stopwatch.ElapsedMilliseconds.ToString());
                return Task.CompletedTask;
            }, context);
            await next();
        }

    }
}
