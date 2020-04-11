using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
namespace serverChallengeMe.Models
{
    public class ChallengeTag
    {
        public int ChallengeID { get; set; }
        public int TagID { get; set; }
        
        public ChallengeTag() { }

        public ChallengeTag(int challengeID, int tagID)
        {
            ChallengeID = challengeID;
            TagID = tagID;
        }

        public DataTable getCT(int tagID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getCT(tagID);
        }

        //public int postStudent(ChallengeTag challengeTag)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.postChallengeTag(challengeTag);
        //}

        //public int deleteChallengeTag(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteChallengeTag(id);
        //}
    }
}