using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using serverChallengeMe.Models;
using serverChallengeMe.Models.DAL;

namespace serverChallengeMe.Controllers
{
    public class AlertController : ApiController
    {
        // GET api/Alert?teacherID_UnRead={teacherID}
        // מחזירה את כמות ההתראות שלא נקראו שיש למורה
        public int getUnReadAlertCount(int teacherID_UnRead)
        {
            Alert alert = new Alert();
            return alert.getUnReadAlertCount(teacherID_UnRead);
        }
        
        // GET api/<controller>
        public DataTable Get(int studentID)
        {
            Alert alert = new Alert();
            return alert.getNumOfAlertNotReadForStudents(studentID);
        }

        // POST api/Alert
        public int Post(Alert alert)
        {
            DBservices dbs = new DBservices();
            return dbs.postAlert(alert);
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