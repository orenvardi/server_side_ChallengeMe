using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
namespace serverChallengeMe.Models
{
    public class Warning
    {
        public int WarningID { get; set; }
        public int TeacherID { get; set; }
        public int StudentID { get; set; }
        public string WarningTitle { get; set; }
        public string WarningText { get; set; }
        public string WarningDate { get; set; }
        public string WarningTime { get; set; }


        public Warning() { }

        public Warning(int warningID, int teacherID, int studentID, string warningTitle , string warningText, string warningDate, string warningTime)
        {
            WarningID = warningID;
            TeacherID = teacherID;
            StudentID = studentID;
            WarningTitle = warningTitle;
            WarningText = warningText;
            WarningDate = warningDate;
            WarningTime = warningTime;
        }

        public DataTable getWarning()
        {
            DBservices dBservices = new DBservices();
            return dBservices.getWarning();
        }

        public int postWarning(Warning warning)
        {
            DBservices dbs = new DBservices();
            return dbs.postWarning(warning);
        }

        public int deleteWarning(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.deleteWarning(id);
        }
    }
}