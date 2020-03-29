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
   
        public StudentChallenge() { }

        public StudentChallenge(int challengeID, int studentID, int difficulty, string deadline, string status)
        {
            ChallengeID = challengeID;
            StudentID = studentID;
            Difficulty = difficulty;
            Deadline = deadline;
            Status = status;
        }

        public int getStudentChallenge(int studentID)
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


        public int deleteStudentChallenge(int studentID, int challengeID)
        {
            DBservices dbs = new DBservices();
            return dbs.deleteStudentChallenge(studentID, challengeID);
        }





    }
}