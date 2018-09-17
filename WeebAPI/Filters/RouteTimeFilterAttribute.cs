using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WeebAPI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RouteTimeFilterAttribute : ActionFilterAttribute
    {
        //Name of the header element being added to store the TIME
        private const string _headerName = "X-API-Timer";

        //Named slot to store the timer abj in the request properties
        private string _property;

        //Private name used to give the timer a unique name
        private string _timerName;

        public RouteTimeFilterAttribute(string TimerName = "")
        {
            _timerName = TimerName;
        }

        //Override OnExegguting
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            //STEP 1: BEFORE Everythinf
            _property = $"{_timerName}_{actionContext.ActionDescriptor.ActionName}";
            actionContext.Request.Properties.Add(_property, Stopwatch.StartNew());
            //SETP 2: Do It
            return base.OnActionExecutingAsync(actionContext, cancellationToken);

            //SETP 3: AFTER Others, but BEFORE this 

        }

        //Override On Exeggcuted
        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            //STEP 1: AFTER everything else, but BEFORE this
            var timer = actionExecutedContext.Request.Properties[_property];

            actionExecutedContext.Response.Headers.Add(_headerName, $"{((Stopwatch)timer).ElapsedMilliseconds.ToString()} ms |{_timerName}");
            //SETP 2: Do It
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
            //SETP 3: AFTER Others, but BEFORE this 
        }
        
    }
}