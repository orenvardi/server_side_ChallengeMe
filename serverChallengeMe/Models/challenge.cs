using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
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

        public DataTable postChallenge(Challenge Challenge, int studentID)
        {
            DBservices dbs = new DBservices();

            //  נוסיף פה חישוב טווחים לפני שעושים אינסרט
            // --start claculate ranges

            // קבלת אחוזי התלמיד
            StudentScore studentScore = dbs.getStudentScore(studentID);
            int teacherEmotional, teacherSocial, teacherSchool = 0;


            // --end claculate ranges

            int newChallengeID = dbs.postChallenge(Challenge);
            return dbs.getChallengeByID(newChallengeID);
        }
    }
}