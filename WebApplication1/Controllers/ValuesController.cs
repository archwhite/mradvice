using AOP;
using ArxOne.MrAdvice.Advice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        public static List<int> List { get; set; } = new List<int>();
        public List<int> list { get; set; } = new List<int>();
        public static int count = 0;

        // GET api/values
        public object Get()
        {
            List.Add(++count);
            list.Add(count);

            return Json(new { Value1 = "value1", Value2 = "value2" });
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
