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

    public class AlertSettingsController : ApiController
    {
        // GET api/AlertSettings
        // מחזיר הגדרות של מחנך לפי המזהה של המחנך
        public AlertSettings GetByTeacherID(int teacherID)
        {
            AlertSettings alertS = new AlertSettings();
            return alertS.getAlertSettingsByTeacherID(teacherID);
        }

        // PUT api/AlertSettings
        public int Put(AlertSettings alertSettings)
        {
            AlertSettings alertS = new AlertSettings();
            return alertS.putAlertSettings(alertSettings);
        }
    }
}