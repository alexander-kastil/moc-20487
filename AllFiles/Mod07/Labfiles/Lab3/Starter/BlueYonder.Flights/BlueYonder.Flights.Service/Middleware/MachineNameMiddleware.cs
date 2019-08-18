using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueYonder.Flights.Service.Middleware
{
    public class MachineNameMiddleware
    {
        private readonly RequestDelegate _next;

        public MachineNameMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("X-BlueYonder-Server", Environment.MachineName);

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MachineNameMiddlewareExtensions
    {
        public static IApplicationBuilder UseMachineNameMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MachineNameMiddleware>();
        }
    }
}
