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

        // מחזירה את כל ההתראות שיש למורה לפי ההגדרות שלו ולפי החיפוש שלו
        public DataTable getTeacherAlertsSearch(int teacherID, string studentName)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getTeacherAlertsSearch(teacherID, studentName);
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


        //----START---התראות אוטומטיות למורה-------
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
        //----END--התראות אוטומטיות מורה--------       

        //----START---התראות אוטומטיות לתלמיד-------
        //1. התראה כל מספר ימים שהתלמיד לא נכנס לאפליקציה 
        public void idleStudentsAlert()
        {
            int idleDays = 3;  // כל 3 ימים תשלח התראה לתלמיד
            DBservices dbs = new DBservices();
            DateTime date = DateTime.Now;
            string alertDate = date.ToString("yyyy-MM-dd");

            DataTable idleStudentsAlert = dbs.idleStudentsAlert(idleDays);
            foreach (DataRow row in idleStudentsAlert.Rows)
            {
                string title = "שכחת ממני";
                string body = "תמשיך לעשות אתגרים כדי שאוכל לגדול";
                string toToken = dbs.getStudentToken(Convert.ToInt32(row["studentID"]));

                Alert alert = new Alert(0, Convert.ToInt32(row["studentID"]), Convert.ToInt32(row["studentID"]), title, body, alertDate, DateTime.Now.ToString("HH:mm"), false, 7);
                postAlert(alert);
                if (toToken != "" && toToken != null)
                    PushNotificationLogic.PushNotification(title, body, toToken);
            }
        }

        //2. התראה עם ספירה לאחור לביצוע האתגר הקרוב אם לא בוצע
        public void preDeadlineStudentsAlert()
        {
            int preDeadlineDays = 5;  // החל מ5 ימים לפני דדליין תשלח התראה לתלמיד בכל יום
            DBservices dbs = new DBservices();
            DateTime date = DateTime.Now;
            string alertDate = date.ToString("yyyy-MM-dd");

            DataTable preDeadlineStudentsAlert = dbs.preDeadlineStudentsAlert(preDeadlineDays);
            foreach (DataRow row in preDeadlineStudentsAlert.Rows)
            {
                string title = "צריך לסיים את האתגר";
                string body = "";
                if (preDeadlineDays == 0)
                {
                    body = row["firstName"] + " " + row["lastName"] + " יש לך רק את היום להשלים את האתגר: " + row["challengeName"];
                }
                if (preDeadlineDays == 1)
                {
                    body = row["firstName"] + " " + row["lastName"] + " נשאר לך יום אחד להשלים את האתגר: " + row["challengeName"];
                }
                if (preDeadlineDays == 2)
                {
                    body = row["firstName"] + " " + row["lastName"] + " נשארו לך יומיים להשלים את האתגר: " + row["challengeName"];
                }
                else
                {
                    body = row["firstName"] + " " + row["lastName"] + " נשארו לך " + row["daysPreDeadline"] + " ימים להשלים את האתגר: " + row["challengeName"];
                }

                string toToken = dbs.getStudentToken(Convert.ToInt32(row["studentID"]));

                Alert alert = new Alert(0, Convert.ToInt32(row["studentID"]), Convert.ToInt32(row["studentID"]), title, body, alertDate, DateTime.Now.ToString("HH:mm"), false, 8);
                postAlert(alert);
                if (toToken != "" && toToken != null)
                    PushNotificationLogic.PushNotification(title, body, toToken);
            }
        }

        //3. התראה כאשר עבר הדדליין והתלמיד לא הצליח את האתגר עם מלל מנחם ומעודד
        public void passDeadlineStudentsAlert()
        {
            DBservices dbs = new DBservices();
            DateTime date = DateTime.Now;
            string alertDate = date.ToString("yyyy-MM-dd");

            DataTable passDeadlineStudentsAlert = dbs.passDeadlineStudentsAlert();
            foreach (DataRow row in passDeadlineStudentsAlert.Rows)
            {
                string title = "אוי לא הצלחת את האתגר בזמן";
                string body = row["firstName"] + " " + row["lastName"] + " היית צריך לסיים את האתגר: " + row["challengeName"] + " עד אתמול ";
                string toToken = dbs.getStudentToken(Convert.ToInt32(row["studentID"]));

                Alert alert = new Alert(0, Convert.ToInt32(row["studentID"]), Convert.ToInt32(row["studentID"]), title, body, alertDate, DateTime.Now.ToString("HH:mm"), false, 9);
                postAlert(alert);
                if (toToken != "" && toToken != null)
                    PushNotificationLogic.PushNotification(title, body, toToken);
            }
        }
        //----END--התראות אוטומטיות תלמיד--------   
        
    }
}