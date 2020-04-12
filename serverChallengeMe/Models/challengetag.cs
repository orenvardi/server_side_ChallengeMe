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

        public int postChallengeTag(List<ChallengeTag> challengeTagArr)
        {
            var x = 0;
            DBservices dbs = new DBservices();
            for (int i = 0; i < challengeTagArr.Count; i++)
            {
                x = dbs.postChallengeTag(challengeTagArr[i]);
            }
            return x;
        }

        //public int deleteChallengeTag(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteChallengeTag(id);
        //}
    }
}