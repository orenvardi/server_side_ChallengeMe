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
        // GET api/Message?teacherID_UnRead={teacherID}
        // מחזירה את כמות ההודעות שלא קראו שיש למורה
        public int getUnReadMessageCount(int teacherID_UnRead)
        {
            Message message = new Message();
            return message.getUnReadMessageCount(teacherID_UnRead);
        }

        // GET api/Message?teacherID_UnRead={teacherID}
        // מחזירה את כמות ההודעות שלא קראו שיש למורה
        public int getStudentUnReadMessageCount(int studentID_UnRead)
        {
            Message message = new Message();
            return message.getStudentUnReadMessageCount(studentID_UnRead);
        }

        // GET api/Message?teacherID={teacherID}
        // מחזירה את המספר המזהה של כל התלמידים שיש למורה הזה הודעות איתם מסודר לפי תאריך ההודעה האחרונה של כל תלמיד
        public List<int> getStudentsWithMessage(int teacherID)
        {
            Message message = new Message();
            return message.getStudentsWithMessage(teacherID);
        }

        // GET api/Message?teacherID={teacherID}&studentID={studentID}
        // מחזירה את כמות ההודעות הנכנסות של המורה הזה מהתלמיד הזה
        public int getMessageTfromSnotRead(int getter_teacherID, int sender_studentID)
        {
            Message message = new Message();
            return message.getMessageTfromSnotRead(getter_teacherID, sender_studentID);
        }

        // GET api/Message?teacherID={teacherID}&studentID={studentID}
        // מחזירה את כל ההודעות שיש בין המורה הזה לתלמיד הזה
        public DataTable getAllMessage(int teacherID, int studentID)
        {
            Message message = new Message();
            return message.getAllMessage(teacherID, studentID);
        }

        // PUT api/Message
        // מעדכן ל: MesgRead=true
        public int Put(int teacherID, int studentID)
        {
            Message m = new Message();
            return m.updateMessage(teacherID, studentID);
        }

        // POST api/<controller>
        public int Post(Message message)
        {
            Message m = new Message();
            return m.postMessage(message);

        }





        // GET api/Message?teacherID={studentID}
        public DataTable Get2(int studentID)
        {
            Message message = new Message();
            return message.getNumOfMessageNotReadForStudents(studentID);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}