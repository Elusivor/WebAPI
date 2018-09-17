using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WeebAPI.Filters;
using WeebAPI.Models;

namespace WeebAPI.Controllers
{
    [RoutePrefix("return")]
    [ClientCacheControlFilter(15,ClientCacheOption.None)]
    public class ReturnTypesController : ApiController
    {
        // GET: api/ReturnTypes
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ReturnTypes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ReturnTypes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ReturnTypes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ReturnTypes/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("void")]
        public  void GetVoid()
        {
        }

        [HttpGet]
        [Route("dto")]
        public ComplexData GetDTO()
        {
            var dto = new ComplexData()
            {
                String1 = "First",
                String2 = "Second",
                Int1 = 1111,
                Int2 = 2222,
                Date1 = DateTime.Now
            };
            return dto;
        }

        [HttpGet]
        [Route("dtoerr")]
        public ComplexData GetDTOError()
        {
            var dto = new ComplexData()
            {
                String1 = "First",
                String2 = "Second",
                Int1 = 1111,
                Int2 = 2222,
                Date1 = DateTime.Now
            };
            throw new InvalidOperationException("Testing exceptions");
        }

        [HttpGet]
        [Route("getDTOJason")]
        public HttpResponseMessage GetDTOAsJason()
        {
            var dto = new ComplexData()
            {
                String1 = "First",
                String2 = "Second",
                Int1 = 1111,
                Int2 = 2222,
                Date1 = DateTime.Now
            };
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json")
            };
            return response;
        }

        [HttpGet]
        [Route("getDTONegotiated")]
        public HttpResponseMessage GetDTOAsRequested()
        {
            var dto = new ComplexData()
            {
                String1 = "First",
                String2 = "Second",
                Int1 = 1111,
                Int2 = 2222,
                Date1 = DateTime.Now
            };
            var response = Request.CreateResponse(dto);

            response.Headers.Add("Some Header", "Has Some Value");
            return response;
        }
    }
}
