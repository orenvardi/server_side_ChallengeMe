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
        public string ChallemgeName { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public Challenge() { }

        public Challenge(int challengeID, string challemgeName, string description, int categoryID)
        {
            ChallengeID = challengeID;
            ChallemgeName = challemgeName;
            Description = Description;
            CategoryID = categoryID;
        }

        //public Challenge getChallenge()
        //{
        //    DBservices dBservices = new DBservices();
        //    return dBservices.getChallenge();
        //}

        //public int postChallenge(Challenge Challenge)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.postChallenge(Challenge);
        //}

        //public int deleteChallenge(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteChallenge(id);
        //}
    }
}