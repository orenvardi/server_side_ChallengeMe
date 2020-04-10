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
        public string Description { get; set; }

        public Challenge() { }

        public Challenge(int challengeID, string challengeName, string description)
        {
            ChallengeID = challengeID;
            ChallengeName = challengeName;
            Description = description;
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

        public int postChallenge(Challenge Challenge)
        {
            DBservices dbs = new DBservices();
            return dbs.postChallenge(Challenge);
        }

        //public int deleteChallenge(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteChallenge(id);
        //}
    }
}