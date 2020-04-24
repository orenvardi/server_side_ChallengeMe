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
    public class MessageController : ApiController
    {
        // GET api/Message?teacherID={teacherID}
        public DataTable Get(int teacherID)
        {
            Message message = new Message();
            return message.getStudentsWithMessage(teacherID);
        }

        // GET api/Message?teacherID={studentID}
        public DataTable Get2(int studentID)
        {
            Message message = new Message();
            return message.getNumOfMessageNotReadForStudents(studentID);
        }
        public DataTable getMessageTfromSnotRead(int teacherID, int studentID)
        {
            Message message = new Message();
            return message.getMessageTfromSnotRead(teacherID,studentID);
        }

        //public DataTable getAllMessage(int teacherID, int studentID)
        //{
        //    Message message = new Message();
        //    return message.getAllMessage(teacherID, studentID);
        //}

        // POST api/<controller>
        public int Post(Message message)
        {
            Message m = new Message();
            return m.postMessage(message);

        }
        // PUT api/<controller>/5
        public int Put(int messageID)
        {
            Message m = new Message();
            return m.updateMessage(messageID);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}