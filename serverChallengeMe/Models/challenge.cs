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
        public double SocialMin { get; set; }
        public double SocialMax { get; set; }
        public double EmotionalMin { get; set; }
        public double EmotionalMax { get; set; }
        public double SchoolMin { get; set; }
        public double SchoolMax { get; set; }
        public bool IsPrivate { get; set; }
             


        public Challenge() { }

        public Challenge(int challengeID, string challengeName, double socialMin, double socialMax, double emotionalMin, double emotionalMax, double schoolMin, double schoolMax, bool isPrivate)
        {
            ChallengeID = challengeID;
            ChallengeName = challengeName;
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

        public DataTable postChallenge(Challenge Challenge)
        {
            DBservices dbs = new DBservices();
            int newChallengeID = dbs.postChallenge(Challenge);
            return dbs.getChallengeByID(newChallengeID);
        }

        //public int deleteChallenge(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteChallenge(id);
        //}
    }
}