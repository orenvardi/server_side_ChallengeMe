using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using serverChallengeMe.Models;

namespace serverChallengeMe.Controllers
{
    public class ChallengeTagController : ApiController
    {

        // GET api/ChallengeTag?tagID={tagID}
        public DataTable Get(int tagID)
        {
            ChallengeTag ct = new ChallengeTag();
            return ct.getCT(tagID);
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