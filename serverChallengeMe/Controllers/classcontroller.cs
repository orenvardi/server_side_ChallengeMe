using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using serverChallengeMe.Models;

namespace serverChallengeMe.Controllers
{
    public class ClassController : ApiController
    {
        // GET api/Class
        public DataTable Get(int teacherID)
        {
            Class c = new Class();
            return c.getClass(teacherID);
        }

        // POST api/<controller>
        public int Post(Class c)
        {
            Class cl = new Class();
            return c.postClass(c);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}