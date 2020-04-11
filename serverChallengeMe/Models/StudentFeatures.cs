using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;

namespace serverChallengeMe.Models
{
    public class StudentFeatures
    {
        public string Answer { get; set; }
        public int StudentID { get; set; }
        public int QuestionID { get; set; }
        public StudentFeatures() { }
        public StudentFeatures(string answer, int studentID, int questionID)
        {
            Answer = answer;
            StudentID = studentID;
            QuestionID = questionID;
        }
    }
}