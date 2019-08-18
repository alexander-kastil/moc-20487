using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueYonderHotels.Service.Attributes
{
    public class CacheAttribute : ActionFilterAttribute
    {
        private string _headerMessage { get; set; }
        private TimeSpan _durationTime;
        private const int _defulatDuration = 60;
        private Dictionary<string,(DateTime, IActionResult)> _cache = new Dictionary<string, (DateTime,IActionResult)>();

        public CacheAttribute(int duration = _defulatDuration)
        {
            _durationTime = new TimeSpan(0, 0, duration);
        }

        public CacheAttribute(string message)
        {
            _durationTime = new TimeSpan(0, 0, _defulatDuration);
            _headerMessage = message;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (CacheValid(context))
            {
                context.Result = _cache[context.HttpContext.Request.Path].Item2;
                return;
            }
            base.OnActionExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            if(!CacheValid(context))
                 _cache[context.HttpContext.Request.Path] = (DateTime.Now,context.Result);

            base.OnResultExecuted(context);
        }

        private bool CacheValid(FilterContext context)
        {
            StringValues xCacheHeader = context.HttpContext.Request.Headers[_headerMessage];
            if (xCacheHeader == "false" || xCacheHeader.Count == 0)
            {
                if (_cache.TryGetValue(context.HttpContext.Request.Path, out (DateTime, IActionResult) cacheValue))
                {
                    if (DateTime.Now - cacheValue.Item1 < _durationTime)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
