using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;

namespace serverChallengeMe.Models
{
    public class AlertSettings
    {
        public int AlertSettingID { get; set; }
        public int TeacherID { get; set; }
        public bool AlertPositive { get; set; }
        public bool AlertNegative { get; set; }
        public bool AlertHelp { get; set; }
        public bool AlertLate { get; set; }
        public int AlertPreDate { get; set; }
        public int AlertIdle { get; set; }

        public AlertSettings() { }

        public AlertSettings(int alertSettingID, int teacherID, bool alertPositive, bool alertNegative, bool alertHelp, bool alertLate, int alertPreDate, int alertIdle)
        {
            AlertSettingID = alertSettingID;
            TeacherID = teacherID;
            AlertPositive = alertPositive;
            AlertNegative = alertNegative;
            AlertHelp = alertHelp;
            AlertLate = alertLate;
            AlertPreDate = alertPreDate;
            AlertIdle = alertIdle;
        }

        //public DataTable getAlertSettings()
        //{
        //    DBservices dBservices = new DBservices();
        //    return dBservices.getAlertSettings();
        //}

        //public int postAlertSettings(AlertSettings alertSettings)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.postAlertSettings(alertSettings);
        //}

        //public int deleteAlertSettings(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteAlertSettings(id);
        //}
    }
}