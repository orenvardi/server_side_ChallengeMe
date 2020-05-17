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

        //public int deleteAlert(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteAlert(id);
        //}

        //----START--התראות אוטומטיות--------
        //1. התראה כאשר עבר דדליין של אתגר והסטטוס שונה מהצליח
        public void passDeadlineAlert()
        {
            DBservices dbs = new DBservices();
            DateTime date = DateTime.Now; 
            string alertDate = date.ToString("yyyy, MMMM dd");

            DataTable passDeadline = dbs.passedDeadlineChallenges();
            foreach (DataRow row in passDeadline.Rows)
            {
                string title = "אתגר לא בוצע בזמן";
                string body = "לתלמיד מכיתה " + row["firstName"] + " " + row["lastName"] + " נגמר הזמן לביצוע האתגר " + row["challengeName"];
                string toToken = dbs.getTeacherToken(Convert.ToInt32(row["teacherID"]));

                Alert alert = new Alert(0, Convert.ToInt32(row["teacherID"]), Convert.ToInt32(row["studentID"]), title, body, alertDate, DateTime.Now.ToString("HH:mm"), false, 0);
                postAlert(alert);
                postToFirebase(title, body, toToken); //לא עובד כרגע
            }
        }

        //2. תלמיד לא נכנס לאפליקציה ___ זמן
        public void idleStudentAlert()
        {
            DBservices dbs = new DBservices();
            DateTime date = DateTime.Now;
            string alertDate = date.ToString("yyyy, MMMM dd");

            DataTable idleStudents = dbs.idleStudents();
            foreach (DataRow row in idleStudents.Rows)
            {
                string title = "תלמיד לא פעיל";
                string body = "התלמיד " + row["firstName"] + " " + row["lastName"] + " לא נכנס לאפליקציה כבר " + row["idleTime"]+" ימים";
                string toToken = dbs.getTeacherToken(Convert.ToInt32(row["teacherID"]));

                Alert alert = new Alert(0, Convert.ToInt32(row["teacherID"]), Convert.ToInt32(row["studentID"]), title, body, alertDate, DateTime.Now.ToString("HH:mm"), false,0);
                postAlert(alert);
                postToFirebase(title, body, toToken);
            }
        }

        //3. לתלמיד נשאר ___ זמן לבצע אתגר
        public void preDeadlineAlert()
        {
            DBservices dbs = new DBservices();
            DateTime date = DateTime.Now;
            string alertDate = date.ToString("yyyy, MMMM dd");

            DataTable preDeadlineChallenges = dbs.preDeadlineChallenges();
            foreach (DataRow row in preDeadlineChallenges.Rows)
            {
                string title = "לתלמיד נשארו ימים ספורים להשלים את האתגר";
                string body = "לתלמיד " + row["firstName"] + " " + row["lastName"] + " נשארו " + row["daysPreDeadline"] + " ימים להשלים את האתגר: "+row["challengeName"];
                string toToken = dbs.getTeacherToken(Convert.ToInt32(row["teacherID"]));

                Alert alert = new Alert(0, Convert.ToInt32(row["teacherID"]), Convert.ToInt32(row["studentID"]), title, body, alertDate, DateTime.Now.ToString("HH:mm"), false,0);
                postAlert(alert);
                postToFirebase(title, body, toToken);
            }
        }
        //----END--התראות אוטומטיות--------


        public int postToFirebase(string title, string body, string toToken)
        {
            return 1;
            /*
             var notification = await {
                "notification": {
                    "title": alertTitle,
                "body": alertText,
                "click_action": "https://challengeme.netlify.app/",
                "icon": "http://url-to-an-icon/icon.png"
                },
            "to": StudentToken
        }
            await fetch("https://fcm.googleapis.com/fcm/send", {
                method: 'POST',
            body: JSON.stringify(notification),
            headers: new Headers({
                'Content-type': 'application/json; charset=UTF-8',
                'Authorization': 'key=AAAAB9pd-t0:APA91bFqlbdOGpqVbNifFlo-_2p9uPFoFqqi0iY5O-_bFjMuzYgVlxC7uC9xRQEprfEqdiDjsNEremg7RWBHlyMQhlhC1Hxo_ZPUsjCYTPUS3nu4cMQJ3tXhUImmftNhg3TPjlN1Wq1G'
            })
        })
            .then(res => {
                 console.log('res=', res);
                 if (!res.ok)
                     throw new Error('Network response was not ok.');
                 return res.json();
             })
            .then(
                (result) => {
                    console.log("fetch POST= ", result);
                },
                (error) => {
                    console.log("err post=", error);
                });
            */
        }
    }
}