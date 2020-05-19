using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using serverChallengeMe.Models;
using System.Web.Http.Cors;

namespace serverChallengeMe.Controllers
{
    [EnableCors("*", "*", "GET, POST, PUT, DELETE")]

    public class StudentScoreController : ApiController
    {
        //GET api/StudentScore?student={student}
        public DataTable Get(int studentID)
        {
            StudentScore studentScore = new StudentScore();
            return studentScore.getStudentScore(studentID);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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