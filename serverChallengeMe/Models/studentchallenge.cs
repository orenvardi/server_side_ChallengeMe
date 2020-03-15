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

        public DataTable getStudentChallenge()
        {
            DBservices dBservices = new DBservices();
            return dBservices.getStudentChallenge();
        }

        public int postStudentChallenge(StudentChallenge studentChallenge)
        {
            DBservices dbs = new DBservices();
            return dbs.postStudentChallenge(studentChallenge);
        }

        public int deleteStudentChallenge(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.deleteStudentChallenge(id);
        }
    }
}