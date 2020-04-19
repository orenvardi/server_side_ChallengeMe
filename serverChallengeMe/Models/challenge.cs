using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
using Newtonsoft.Json.Linq;

namespace serverChallengeMe.Models
{
    public class Challenge
    {
        public int ChallengeID { get; set; }
        public string ChallengeName { get; set; }
        public int Difficulty { get; set; }
        public double SocialMin { get; set; }
        public double SocialMax { get; set; }
        public double EmotionalMin { get; set; }
        public double EmotionalMax { get; set; }
        public double SchoolMin { get; set; }
        public double SchoolMax { get; set; }
        public bool IsPrivate { get; set; }
             


        public Challenge() { }

        public Challenge(int challengeID, string challengeName, int difficulty, double socialMin, double socialMax, double emotionalMin, double emotionalMax, double schoolMin, double schoolMax, bool isPrivate)
        {
            ChallengeID = challengeID;
            ChallengeName = challengeName;
            Difficulty = difficulty;
            SocialMin = socialMin;
            SocialMax = socialMax;
            EmotionalMin = emotionalMin;
            EmotionalMax = emotionalMax;
            SchoolMin = schoolMin;
            SchoolMax = schoolMax;
            IsPrivate = isPrivate;
        }

        public DataTable getChallenge()
        {
            DBservices dBservices = new DBservices();
            return dBservices.getChallenge();
        }

        public DataTable getChallengeByName(string challengeName)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getChallengeByName(challengeName);
        }

        public DataTable postChallenge(JObject data)
        {
            DBservices dbs = new DBservices();

            // הוצאת כל הערכים מהאובייקט שהתקבל
            string challengeName = (string)data["challengeName"];
            bool isPrivate = (bool)data["isPrivate"];
            int difficulty = (int)data["difficulty"];
            int studentID = (int)data["studentID"];
            int T_emotional = (int)data["emotional"];
            int T_social = (int)data["social"];
            int T_school = (int)data["school"];

            // משיכת אחוזי התלמיד מהדאטה בייס
            StudentScore studentScore = dbs.getStudentScore(studentID);
            double S_emotional = studentScore.Emotional;
            double S_social = studentScore.Social;
            double S_school = studentScore.School;

            //  חישוב טווחים לפני שעושים אינסרט
            // --start claculate ranges
            double d_emotional = 150.17 * Math.Exp(-0.034 * T_emotional);
            double d_emotional_Min = d_emotional / difficulty;
            double d_emotional_Max = d_emotional / (6 - difficulty);

            double d_social = 150.17 * Math.Exp(-0.034 * T_social);
            double d_social_Min = d_social / difficulty;
            double d_social_Max = d_social / (6 - difficulty);

            double d_school = 150.17 * Math.Exp(-0.034 * T_school);
            double d_school_Min = d_school / difficulty;
            double d_school_Max = d_school / (6 - difficulty);

            double emotionalMin = (S_emotional - d_emotional_Min) < 0 ? 0 : (S_emotional - d_emotional_Min);
            double emotionalMax = (S_emotional + d_emotional_Max) > 100 ? 100 : (S_emotional + d_emotional_Max);
            double socialMin = (S_social - d_social_Min) < 0 ? 0 : (S_social - d_social_Min);
            double socialMax = (S_social + d_social_Max) > 100 ? 100 : (S_social + d_social_Max);
            double schoolMin = (S_school - d_school_Min) < 0 ? 0 : (S_school - d_school_Min);
            double schoolMax = (S_school + d_school_Max) > 100 ? 100 : (S_school + d_school_Max);

            // אם המורה הכניס 0 לאחת מהקטגוריות אז משנים את הטווח ל0 עד 100
            if (T_emotional==0 || T_social==0 || T_school == 0)
            {
                emotionalMin = (T_emotional == 0 ? 0 : emotionalMin);
                emotionalMax = (T_emotional == 0 ? 100 : emotionalMin);
                socialMin = (T_social == 0 ? 0 : socialMin);
                socialMax = (T_social == 0 ? 100 : socialMax);
                schoolMin = (T_school == 0 ? 0 : schoolMin);
                schoolMax = (T_school == 0 ? 100 : schoolMax);
            }
            // --end claculate ranges

            int challengeID = 0;

            // הפעלת הפונקציה שעושה אינסרט לשרת ופקודה שמחזירה את האובייקט שנוצר
            Challenge challenge = new Challenge(challengeID, challengeName, difficulty, socialMin, socialMax, emotionalMin, emotionalMax, schoolMin, schoolMax, isPrivate);
            int newChallengeID = dbs.postChallenge(challenge);
            return dbs.getChallengeByID(newChallengeID);
        }
    }
}