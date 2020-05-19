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

    public class StudentChallengeController : ApiController
    {
        //GET api/StudentChallenge?studentID ={studentID}
        public DataTable Get(int studentID)
        {
            StudentChallenge stuC = new StudentChallenge();
            return stuC.getStudentChallenge(studentID);
        }

        //GET api/StudentChallenge?studentID ={studentID}
        public string Get(int studentID,int challengeID)
        {
            StudentChallenge stuC = new StudentChallenge();
            return stuC.getChallengeImage(studentID, challengeID);
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


        //string image, int challengeID, int studentID
        // PUT api/<controller>/5
        // שמירת התמונה שהתלמיד בחר לאתגר
        [HttpPut]
        [Route("api/StudentChallenge/AddImg")]
        public int Put2(StudentChallenge sc)
        {
            StudentChallenge stuC = new StudentChallenge();
            return stuC.putChallengeImage(sc);
        }

        // PUT api/StudentChallenge?challengeID={challengeID}&studentID={studentID}&status={status}
        // עדכון סטטוס כאשר התלמיד לוחץ
        public int Put(int challengeID, int studentID, string status)
        {
            StudentChallenge stuC = new StudentChallenge();
            return stuC.updateStatus(challengeID, studentID, status);
        }

        // DELETE api/<controller>/5
        public int Delete(int studentID, int challengeID)
        {
            StudentChallenge stu = new StudentChallenge();
            return stu.deleteStudentChallenge(studentID, challengeID);
        }
    }
}