﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using serverChallengeMe.Models;

namespace serverChallengeMe.Controllers
{
    public class StudentChallengeController : ApiController
    {
        //GET api/StudentChallenge?studentID ={studentID}
        public DataTable Get(int studentID)
        {
            StudentChallenge stuC = new StudentChallenge();
            return stuC.getStudentChallenge(studentID);
        }

        // POST api/<controller>
        public int Post(StudentChallenge sc)
        {
            StudentChallenge stuC = new StudentChallenge();
            return stuC.postStudentChallenge(sc);
        }

        // PUT api/<controller>/5
        public int Put(StudentChallenge sc)
        {
            StudentChallenge stuC = new StudentChallenge();
            return stuC.putStudentChallenge(sc);
        }

        // DELETE api/<controller>/5
        public int Delete(int studentID, int challengeID)
        {
            StudentChallenge stu = new StudentChallenge();
            return stu.deleteStudentChallenge(studentID, challengeID);
        }
    }
}