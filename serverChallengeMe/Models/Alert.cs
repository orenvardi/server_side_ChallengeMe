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
        public bool AlertRead { get; set; }

        public Alert() { }

        public Alert(int alertID, int teacherID, int studentID, string alertTitle, string alertText, string alertDate, string alertTime, bool alertRead)
        {
            AlertID = alertID;
            TeacherID = teacherID;
            StudentID = studentID;
            AlertTitle = alertTitle;
            AlertText = alertText;
            AlertDate = alertDate;
            AlertTime = alertTime;
            AlertRead = alertRead;
        }

        // מחזירה את כמות ההודעות שלא נקראו שיש למורה
        public int getUnReadAlertCount(int teacherID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getUnReadAlertCount(teacherID);
        }

        public DataTable getNumOfAlertNotReadForStudents(int studentID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getNumOfAlertNotReadForStudents(studentID);
        }

        public int postAlert(Alert alert)
        {
            DBservices dbs = new DBservices();
            return dbs.postAlert(alert);
        }

        //public int deleteAlert(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteAlert(id);
        //}
    }
}