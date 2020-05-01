using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using serverChallengeMe.Models.DAL;
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
        public int putChallengeImage(StudentChallenge sc)
        {
            DBservices dbs = new DBservices();
            //string folderPath = Server.MapPath("~/ImagesFolder/");  //Create a Folder in your Root directory on your solution.
            string fileName = sc.ChallengeID + ".png";
            string imagePath = "C:\\Users\\user\\Desktop\\ChallengeMeClient\\src\\img" + fileName;

            string base64StringData = sc.Image; // Your base 64 string data
            string cleandata = base64StringData.Replace("data:image/png;base64,", "");
            byte[] data = System.Convert.FromBase64String(cleandata);
            MemoryStream ms = new MemoryStream(data);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            img.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
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