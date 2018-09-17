using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeebAPI.Filters;

namespace WeebAPI.Controllers
{
    [RoutePrefix("Trafficking")]
    public class TraffickingController : ApiController
    {
        // GET: api/Trafficking
        [HttpGet,Route("")]
        [RouteTimeFilter("Trafficking")]
        [ClientCacheControlFilter(15,ClientCacheOption.Private)]
        public IEnumerable<string> Get()
        {
            return new string[] { "Georgina Dolly Fish (26)", "Chloe Beth Chan (26)" };
        }

        [HttpGet,Route("{id:int}")]
        // GET: api/Trafficking/5
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet, Route("{id}")]
        // GET: api/Trafficking/5
        public string Get(string id)
        {
            return $"value {id}";
        }
        [HttpPost,Route("")]
        // POST: api/Trafficking
        public void Post([FromBody]string value)
        {
        }
        [HttpPost, Route("{id:int}")]
        // PUT: api/Trafficking/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Trafficking/5
        public void Delete(int id)
        {
        }
    }
}
