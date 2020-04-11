using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;

namespace serverChallengeMe.Models
{
    public class FeaturesQuestion
    {
        public int QuestionID { get; set; }
        public string Question { get; set; }
        public int CategoryID { get; set; }
        public FeaturesQuestion() { }
        public FeaturesQuestion(int QuestionID, string question, int categoryID)
        {
            QuestionID = questionID;
            Question = question;
            CategoryID = categoryID;
        }

    }
}