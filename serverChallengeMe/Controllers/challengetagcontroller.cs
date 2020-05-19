using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using serverChallengeMe.Models;
using System.Web.Http.Cors;

namespace serverChallengeMe.Controllers
{
    [EnableCors("*", "*", "GET, POST, PUT, DELETE")]

    public class ChallengeTagController : ApiController
    {

        // GET api/ChallengeTag?tagID={tagID}
        public DataTable Get(string SrtTagsID)
        {
            int[] IntTagIDArr = SrtTagsID.Split(',').Select(str => Int32.Parse(str)).ToArray();
            ChallengeTag ct = new ChallengeTag();
            return ct.getCT(IntTagIDArr);
        }

        // POST api/ChallengeTag
        public int Post(List<ChallengeTag> challengeTagArr)
        {
            ChallengeTag challengeTag = new ChallengeTag();
            return challengeTag.postChallengeTag(challengeTagArr);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}