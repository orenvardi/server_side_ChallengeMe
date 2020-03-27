using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
namespace serverChallengeMe.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        public string TagName { get; set; }

        public Tag() { }

        public Tag(int tagID, string tagName)
        {
            TagID = tagID;
            TagName = tagName;
        }

        public DataTable getTag()
        {
            DBservices dBservices = new DBservices();
            return dBservices.getTag();
        }

        //public int postTag(Tag tag)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.postTag(tag);
        //}

        //public int deleteTag(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteTag(id);
        //}
    }
}