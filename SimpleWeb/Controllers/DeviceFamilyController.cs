using SimpleWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleWeb.Controllers
{
    public class DeviceFamilyController : ApiController
    {
        // GET: api/DeviceFamily
        public IEnumerable<DeviceFamilyInfo> Get()
        {
            return DeviceFamilyStore.Get();
        }

        // GET: api/DeviceFamily/5
        public string Get(string id)
        {
            DeviceFamilyStore.Register(id);
            return "OK";
        }

        // POST: api/DeviceFamily
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DeviceFamily/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DeviceFamily/5
        public void Delete(int id)
        {
        }
    }
}
