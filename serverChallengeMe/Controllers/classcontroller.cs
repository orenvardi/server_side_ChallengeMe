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
            return cl.postClass(c);
        }

        // PUT api/<controller>/5
        public int Put(Class c)
        {
            Class cl = new Class();
            return cl.putClass(c);
        }

        // DELETE api/<controller>/5
        public int Delete(int classID)
        {
            Class stu = new Class();
            return stu.deleteClass(classID);
        }
    }
}