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
            DataTable SmartElementOffer = dBservices.findSmartElementOffer(studentScore);
            
            return SmartElementOffer;
            
            //DataTable matchStudentToStudent5 = dBservices.matchStudentToStudent(studentScore);
            //matchStudentToChallenge4.Merge(matchStudentToStudent5, true, MissingSchemaAction.Add);
            //DataTable distinctTable = matchStudentToChallenge4.DefaultView.ToTable( /*distinct*/ true);
        }
    }
}