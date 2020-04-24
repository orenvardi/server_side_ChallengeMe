using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
namespace serverChallengeMe.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public int TeacherID { get; set; }
        public int StudentID { get; set; }
        public string MessageTitle { get; set; }
        public string MessageText { get; set; }
        public string MessageDate { get; set; }
        public string MessageTime { get; set; }
        public bool MessageByTeacher { get; set; }


        public Message() { }

        public Message(int messageID, int teacherID, int studentID, string messageTitle, string messageText, string messageDate, string messageTime, bool messageByTeacher)
        {
            MessageID = messageID;
            TeacherID = teacherID;
            StudentID = studentID;
            MessageTitle = messageTitle;
            MessageText = messageText;
            MessageDate = messageDate;
            MessageTime = messageTime;
            MessageByTeacher = messageByTeacher;
        }
        public DataTable getMessageTfromSnotRead(int teacherID, int studentID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getMessageTfromSnotRead(teacherID, studentID);
        }
        public DataTable getStudentsWithMessage(int teacherID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getStudentsWithMessage(teacherID);
        }

        public DataTable getNumOfMessageNotReadForStudents(int studentID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getNumOfMessageNotReadForStudents(studentID);
        }

        //public DataTable getAllMessage(int teacherID, int studentID)
        //{
        //    DBservices dBservices = new DBservices();
        //    return dBservices.getAllMessage(teacherID, studentID);
        //}

        public int postMessage(Message message)
        {
            DBservices dbs = new DBservices();
            return dbs.postMessage(message);
        }
        public int updateMessage(int messageID)
        {
            DBservices dbs = new DBservices();
            return dbs.updateMessage(messageID);
        }

        //public int deleteMessage(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteMessage(id);
        //}
    }
}