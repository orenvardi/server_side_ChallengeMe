using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
using serverChallengeMe.Models.FCM;

namespace serverChallengeMe.Models
{
    public class Transfer
    {
        public int TransferID { get; set; }
        public int TeacherFrom { get; set; }
        public int TeacherTo { get; set; }
        public int StudentID { get; set; }
        public string Comment { get; set; }
        public bool Confirm { get; set; }

        public Transfer() { }

        public Transfer(int transferID, int teacherFrom, int teacherTo, int studentID, string comment, bool confirm)
        {
            TransferID = transferID;
            TeacherFrom = teacherFrom;
            TeacherTo = teacherTo;
            StudentID = studentID;
            Comment = comment;
            Confirm = confirm;
        }

        // מחזירה את כל ההעברות אל המורה שלא אושרו
        public DataTable getTransfersToTeacher(int teacherID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getTransfersToTeacher(teacherID);
        }

        // מחזירה את כל ההעברות מהמורה
        public DataTable getTransfersFromTeacher(int teacherID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getTransfersFromTeacher(teacherID);
        }

        // מעדכן את העמודה קונפירם לטרו
        public int confirmTransfer(int transferID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.confirmTransfer(transferID);
        }

        public int postTransfer(Transfer transfer)
        {
            DBservices dBservices = new DBservices();
            return dBservices.postTransfer(transfer);
        }
    }
}