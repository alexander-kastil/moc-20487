using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CustomFiltersAndFormatters.Filters
{
    public class AddHeaderFilter : IResultFilter
    {
        private ILogger _logger;
        public AddHeaderFilter
            (ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.
                CreateLogger<AddHeaderFilter>();
        }

        public void OnResultExecuting
            (ResultExecutingContext context)
        {
            var headerName = "OnResultExecuting";
            context.HttpContext.Response.
                Headers.Add(
                    headerName, new string[]
                        { "ResultExecutingSuccessfully" });
            _logger.LogInformation
                ($"Header added: {headerName}");
        }

        public void OnResultExecuted
            (ResultExecutedContext context)
        {
            // Can't add to headers here because the response has already begun.
        }
    }
}
