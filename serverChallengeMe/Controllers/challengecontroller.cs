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
    public class ChallengeController : ApiController
    {
        // GET api/Challenge
        public DataTable Get()
        {
            Challenge c = new Challenge();
            return c.getChallenge();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post(Challenge challenge)
        {
            Challenge c = new Challenge();
            return c.postChallenge(challenge);
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