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

    public class TransferController : ApiController
    {
        [HttpGet]
        [Route("api/Transfer/getTransfersToTeacher")]
        // מחזירה את כל ההעברות אל המורה שלא אושרו
        // GET api/Transfer
        public DataTable getTransfersToTeacher(int teacherID)
        {
            Transfer transfer = new Transfer();
            return transfer.getTransfersToTeacher(teacherID);
        }

        [HttpGet]
        [Route("api/Transfer/getTransfersFromTeacher")]
        // מחזירה את כל ההעברות מהמורה
        // GET api/Transfer
        public DataTable getTransfersFromTeacher(int teacherID)
        {
            Transfer transfer = new Transfer();
            return transfer.getTransfersFromTeacher(teacherID);
        }

        [HttpGet]
        [Route("api/Transfer/confirmTransfer")]
        // מעדכן את העמודה קונפירם לטרו
        // PUT api/Transfer
        public int confirmTransfer(int transferID)
        {
            Transfer t = new Transfer();
            return t.confirmTransfer(transferID);
        }
        
        // POST api/Transfer
        public int Post(Transfer transfer)
        {
            Transfer t = new Transfer();
            return t.postTransfer(transfer);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}