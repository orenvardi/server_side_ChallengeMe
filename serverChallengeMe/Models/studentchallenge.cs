using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
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