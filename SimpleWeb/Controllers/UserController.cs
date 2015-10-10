using SimpleWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleWeb.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public IEnumerable<string> Get()
        {
            return UserStore.Users;
        }

        // GET: api/User/5
        public string Get(string id)
        {
            if (!UserStore.Users.Exists(p=>p==id)) UserStore.Users.Add(id);
            return "OK";
        }

        // GET: api/delete/user
        public string Get(string cmd, string id)
        {
            switch(cmd)
            {
                case "register":
                    if (!UserStore.Users.Exists(p => p == id)) UserStore.Users.Add(id);
                    break;
                case "delete":
                    UserStore.Users.Remove(id);
                    break;
            }
            return "OK";
        }

        // POST: api/User
        public string Post([FromBody]string value)
        {
            return "Received " + value;
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
