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
    public class StudentFeaturesController : ApiController
    {
        // GET api/StudentFeatures
        public DataTable Get(int studentID)
        {
            StudentFeatures fq = new StudentFeatures();
            return fq.getFQBystudentID(studentID);
        }

        // GET api/StudentFeatures answers&questions
        public DataTable GetQuestionsAndAnswers(int studentID2)
        {
            StudentFeatures fq = new StudentFeatures();
            return fq.getQuestionsAndAnswers(studentID2);
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