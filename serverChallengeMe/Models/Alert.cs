using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
namespace serverChallengeMe.Models
{
    public class Alert
    {
        public int AlertID { get; set; }
        public int TeacherID { get; set; }
        public int StudentID { get; set; }
        public string AlertTitle { get; set; }
        public string AlertText { get; set; }
        public string AlertDate { get; set; }
        public string AlertTime { get; set; }


        public Alert() { }

        public Alert(int alertID, int teacherID, int studentID, string alertTitle, string alertText, string alertDate, string alertTime)
        {
            AlertID = alertID;
            TeacherID = teacherID;
            StudentID = studentID;
            AlertTitle = alertTitle;
            AlertText = alertText;
            AlertDate = alertDate;
            AlertTime = alertTime;
        }

        //public DataTable getAlert()
        //{
        //    DBservices dBservices = new DBservices();
        //    return dBservices.getAlert();
        //}

        //public int postAlert(Alert alert)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.postAlert(alert);
        //}

        //public int deleteAlert(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteAlert(id);
        //}
    }
}