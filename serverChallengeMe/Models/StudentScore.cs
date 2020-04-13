using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
using serverChallengeMe.Models;

namespace serverChallengeMe.Models
{
    public class StudentScore
    {
        public int StudentID { get; set; }
        public double Social { get; set; }
        public double School { get; set; }
        public double Emotional { get; set; }
        public StudentScore() { }
        public StudentScore(int studentID, double social, double school, double emotional)
        {
            StudentID = studentID;
            Social = social;
            School = school;
            Emotional = emotional;
        }

        public DataTable getStudentScore(int studentID)
        {
            DBservices dBservices = new DBservices();
            //פונקציה שמחזירה את ציוני התלמיד
            StudentScore studentScore = dBservices.getStudentScore(studentID);

            // פונקציה שמחזירה את האתגרים המתאימים לילד מטבלת אתגרים
            DataTable matchStudentToChallenge = dBservices.matchStudentToChallenge(studentScore);
            DataTable distinctTable = matchStudentToChallenge.DefaultView.ToTable( /*distinct*/ true);
            DataTable matchStudentToStudent = dBservices.matchStudentToStudent(studentScore);
            matchStudentToChallenge.Merge(matchStudentToStudent, true, MissingSchemaAction.Add);
            return matchStudentToChallenge;

        }
    }
}