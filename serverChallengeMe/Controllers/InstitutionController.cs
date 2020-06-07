using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Web.Http;
using serverChallengeMe.Models;

namespace serverChallengeMe.Controllers
{
    public class InstitutionController : ApiController
    {
        // GET api/Institution
        public DataTable Get()
        {
            Institution ins = new Institution();
            return ins.getInstitutions();
        }
    }
}