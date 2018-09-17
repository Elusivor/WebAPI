using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http.Headers;
namespace WeebAPI.Filters
{
    public enum ClientCacheOption
    {
        None,
        Public,
        Private
    };
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]

    public class ClientCacheControlFilter : ActionFilterAttribute
    {
        private ClientCacheOption _option;
        private int _cacheForSeconds;
        public ClientCacheControlFilter() : this(10, ClientCacheOption.Private) { }
        public ClientCacheControlFilter(int CacheForSeconds) : this(CacheForSeconds, ClientCacheOption.Private) { }
        public ClientCacheControlFilter(int CacheForSeconds, ClientCacheOption Option)
        {
            _option = Option;
            _cacheForSeconds = CacheForSeconds;
        }

        
        //Override On Exeggcuted
        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            //STEP 1: AFTER everything else, but BEFORE this
           
            //SETP 2: Do It
            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
            //SETP 3: AFTER Others, but BEFORE this 
            if (_option == ClientCacheOption.None)
            {
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoStore = true
                };
                actionExecutedContext.Response.Headers.Pragma.TryParseAdd("no-cache");
                if (!actionExecutedContext.Response.Headers.Date.HasValue)
                    actionExecutedContext.Response.Headers.Date = DateTime.UtcNow;
                actionExecutedContext.Response.Content.Headers.Expires = actionExecutedContext.Response.Headers.Date;
            }
            else
            {
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoStore = false,
                    Private = (_option == ClientCacheOption.Private),
                    Public = (_option == ClientCacheOption.Public),
                    MaxAge = TimeSpan.FromSeconds(_cacheForSeconds)
                };

                if (!actionExecutedContext.Response.Headers.Date.HasValue)
                    actionExecutedContext.Response.Headers.Date = DateTime.UtcNow;

                actionExecutedContext.Response.Content.Headers.Expires = DateTime.UtcNow.AddSeconds(_cacheForSeconds);
            }
        }
    }
}