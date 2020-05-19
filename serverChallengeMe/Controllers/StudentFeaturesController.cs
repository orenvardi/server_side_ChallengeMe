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
        public int Post(List<StudentFeatures> StudentFeaturesArr)
        {
            StudentFeatures SF = new StudentFeatures();
            return SF.postStudentFeatures(StudentFeaturesArr);
        }

        // POST api/<controller>
        public int Put(List<StudentFeatures> StudentFeaturesArr)
        {
            StudentFeatures SF = new StudentFeatures();
            return SF.putStudentFeatures(StudentFeaturesArr);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}