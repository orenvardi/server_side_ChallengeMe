using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;

namespace serverChallengeMe.Models
{
    public class WarningSettings
    {
        public int WarningSettingID { get; set; }
        public int TeacherID { get; set; }
        public bool WarningPositive { get; set; }
        public bool WarningNegative { get; set; }
        public bool WarningHelp { get; set; }
        public bool WarningLate { get; set; }
        public int WarningPreDate { get; set; }
        public int WarningIdle { get; set; }

        public WarningSettings() { }

        public WarningSettings(int warningSettingID, int teacherID, bool warningPositive, bool warningNegative, bool warningHelp, bool warningLate, int warningPreDate, int warningIdle)
        {
            WarningSettingID = warningSettingID;
            TeacherID = teacherID;
            WarningPositive = warningPositive;
            WarningNegative = warningNegative;
            WarningHelp = warningHelp;
            WarningLate = warningLate;
            WarningPreDate = warningPreDate;
            WarningIdle = warningIdle;
        }

        //public DataTable getWarningSettings()
        //{
        //    DBservices dBservices = new DBservices();
        //    return dBservices.getWarningSettings();
        //}

        //public int postWarningSettings(WarningSettings warningSettings)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.postWarningSettings(warningSettings);
        //}

        //public int deleteWarningSettings(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteWarningSettings(id);
        //}
    }
}