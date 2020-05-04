using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using serverChallengeMe.Models.DAL;
using serverChallengeMe.Models;

namespace serverChallengeMe.Models
{
    public class StudentChallenge
    {
        public int ChallengeID { get; set; }
        public int StudentID { get; set; }
        public int Difficulty { get; set; }
        public string Deadline { get; set; }
        public string Status { get; set; }
        public string TimeStamp { get; set; }
        public string Image { get; set; }

        public StudentChallenge() { }

        public StudentChallenge(int challengeID, int studentID, int difficulty, string deadline, string status, string timeStamp, string image)
        {
            ChallengeID = challengeID;
            StudentID = studentID;
            Difficulty = difficulty;
            Deadline = deadline;
            Status = status;
            TimeStamp = timeStamp;
            Image = image;
        }

        public DataTable getStudentChallenge(int studentID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getStudentChallenge(studentID);
        }

        public string getChallengeImage(int studentID, int challengeID)
        {
            DBservices dBservices = new DBservices();
            // לוקחים את נתיב התמונה ששמור כסטרינג בטבלה בדאטה בייס
            string imagePath = dBservices.getChallengeImage(studentID, challengeID);
            // ממירים את נתיב התמונה מסטרינג למערך של ביטים
            byte[] imageArray = System.IO.File.ReadAllBytes(imagePath);
            // ממירים את מערך הביטים לבייס 64
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            // מחזירים את התמונה לצד לקוח כבייס 64
            return base64ImageRepresentation;
        }

        public int postStudentChallenge(StudentChallenge sc)
        {
            DBservices dbs = new DBservices();
            return dbs.postStudentChallenge(sc);
        }

        public int putStudentChallenge(StudentChallenge sc)
        {
            DBservices dbs = new DBservices();
            return dbs.updateStudentChallenge(sc);
        }

        //string image, int challengeID, int studentID
        // שמירת נתיב תמונת האתגר בדאטה בייס
        public int putChallengeImage(StudentChallenge sc)
        {
            // התמונה מתקבלת מהצד לקוח כבייס 64, שומרים אותה במשתנה
            string base64StringData = sc.Image; // Your base 64 string data

            //remove everything before the first /
            string type = base64StringData.Substring(base64StringData.IndexOf("/")+1);
            
            // remove everything after the first /
            type = type.Substring(0, type.IndexOf(";"));

            // מגדירים ששם הקובץ יהיה המספר המזהה של האתגר עם הסיומת המתאימה
            string fileName = sc.ChallengeID.ToString() + 's' + sc.StudentID.ToString() + "."+type;
           
            // נתיב התמונה נלקח מהמחלקה הסטטית שלנו בתוספת שם הקובץ
            string imagePath = PathOfImage.path + fileName;
            
            // חותכים את התחלת הסטרינג כי זה מיותר
            string cleandata = base64StringData.Replace("data:image/"+ type + ";base64,", "");
            
            // עושים המרה מבייס 64 למערך של ביטים
            byte[] data = System.Convert.FromBase64String(cleandata);

            MemoryStream ms = new MemoryStream(data);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            // שומרים את התמונה בנתיב שהגדרנו
            img.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            // שומרים בטבלה בדאטה בייס את נתיב התמונה
            DBservices dbs = new DBservices();
            return dbs.putChallengeImage(imagePath, sc.ChallengeID, sc.StudentID);
        }

        public int updateStatus (int challengeID, int studentID, string status)
        {
            DBservices dbs = new DBservices();
            return dbs.updateStatus(challengeID, studentID, status);
        }
        public int deleteStudentChallenge(int studentID, int challengeID)
        {
            DBservices dbs = new DBservices();
            return dbs.deleteStudentChallenge(studentID, challengeID);
        }
    }
}