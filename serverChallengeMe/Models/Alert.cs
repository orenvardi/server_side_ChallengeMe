using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
using serverChallengeMe.Models.FCM;

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
        public int AlertTypeID { get; set; }

        public Alert() { }

        public Alert(int alertID, int teacherID, int studentID, string alertTitle, string alertText, string alertDate, string alertTime, bool alertRead, int alertTypeID)
        {
            AlertID = alertID;
            TeacherID = teacherID;
            StudentID = studentID;
            AlertTitle = alertTitle;
            AlertText = alertText;
            AlertDate = alertDate;
            AlertTime = alertTime;
            AlertRead = alertRead;
            AlertTypeID = alertTypeID;
        }

        // מחזירה את כמות ההתראות שלא נקראו שיש למורה
        public int getUnReadAlertCount(int teacherID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getUnReadAlertCount(teacherID);
        }

        // מחזירה את כל ההתראות שיש למורה לפי ההגדרות שלו
        public DataTable getTeacherAlerts(int teacherID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getTeacherAlerts(teacherID);
        }

        // מחזירה את כמות ההתראות שלא נקראו שיש לתלמיד
        public DataTable getNumOfAlertNotReadForStudents(int studentID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getNumOfAlertNotReadForStudents(studentID);
        }

        //יצירת התראה בדאטה בייס
        public int postAlert(Alert alert)
        {
            DBservices dbs = new DBservices();
            return dbs.postAlert(alert);
        }

        // מעדכן עבור המורה את ההתראה לנקראה
        public int putAlertToRead(int alertID)
        {
            DBservices dbs = new DBservices();
            return dbs.putAlertToRead(alertID);
        }

        // מחיקת התראה
        public int deleteAlert(int alertID)
        {
            DBservices dbs = new DBservices();
            return dbs.deleteAlert(alertID);
        }


        //----START--התראות אוטומטיות--------
        //1. התראה כאשר עבר דדליין של אתגר והסטטוס שונה מהצליח
        public void passDeadlineAlert()
        {
            DBservices dbs = new DBservices();
            DateTime date = DateTime.Now; 
            string alertDate = date.ToString("yyyy-MM-dd");

            DataTable passDeadline = dbs.passedDeadlineChallenges();
            foreach (DataRow row in passDeadline.Rows)
            {
                string title = "אתגר לא בוצע בזמן";
                string body = "לתלמיד מכיתה " + row["firstName"] + " " + row["lastName"] + " נגמר הזמן לביצוע האתגר " + row["challengeName"];
                string toToken = dbs.getTeacherToken(Convert.ToInt32(row["teacherID"]));

                Alert alert = new Alert(0, Convert.ToInt32(row["teacherID"]), Convert.ToInt32(row["studentID"]), title, body, alertDate, DateTime.Now.ToString("HH:mm"), false, 4);
                postAlert(alert);
                if (toToken != "" && toToken != null)
                    PushNotificationLogic.PushNotification(title, body, toToken);
            }
        }

        //2. תלמיד לא נכנס לאפליקציה ___ זמן
        public void idleStudentAlert()
        {
            DBservices dbs = new DBservices();
            DateTime date = DateTime.Now;
            string alertDate = date.ToString("yyyy-MM-dd");

            DataTable idleStudents = dbs.idleStudents();
            foreach (DataRow row in idleStudents.Rows)
            {
                string title = "תלמיד לא פעיל";
                string body = "התלמיד " + row["firstName"] + " " + row["lastName"] + " לא נכנס לאפליקציה כבר " + row["idleTime"]+" ימים";
                string toToken = dbs.getTeacherToken(Convert.ToInt32(row["teacherID"]));

                Alert alert = new Alert(0, Convert.ToInt32(row["teacherID"]), Convert.ToInt32(row["studentID"]), title, body, alertDate, DateTime.Now.ToString("HH:mm"), false,6);
                postAlert(alert);
                if (toToken != "" && toToken != null)
                    PushNotificationLogic.PushNotification(title, body, toToken);
            }
        }

        //3. לתלמיד נשאר ___ זמן לבצע אתגר
        public void preDeadlineAlert()
        {
            DBservices dbs = new DBservices();
            DateTime date = DateTime.Now;
            string alertDate = date.ToString("yyyy-MM-dd");

            DataTable preDeadlineChallenges = dbs.preDeadlineChallenges();
            foreach (DataRow row in preDeadlineChallenges.Rows)
            {
                string title = "לתלמיד נשארו ימים ספורים להשלים את האתגר";
                string body = "לתלמיד " + row["firstName"] + " " + row["lastName"] + " נשארו " + row["daysPreDeadline"] + " ימים להשלים את האתגר: "+row["challengeName"];
                string toToken = dbs.getTeacherToken(Convert.ToInt32(row["teacherID"]));

                Alert alert = new Alert(0, Convert.ToInt32(row["teacherID"]), Convert.ToInt32(row["studentID"]), title, body, alertDate, DateTime.Now.ToString("HH:mm"), false,5);
                postAlert(alert);
                if (toToken != "" && toToken != null)
                    PushNotificationLogic.PushNotification(title, body, toToken);
            }
        }
        //----END--התראות אוטומטיות--------       
    }
}