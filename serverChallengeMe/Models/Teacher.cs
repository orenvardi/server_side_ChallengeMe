using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
using System.Web.Security;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace serverChallengeMe.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string School { get; set; }
        public bool TempPassword { get; set; }
        public string TeacherToken { get; set; }

        public Teacher() { }

        public Teacher(int teacherID, string userName, string password, string firstName, string lastName, string phone, string mail, string school, bool tempPassword, string teacherToken)
        {
            TeacherID = teacherID;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Mail = mail;
            School = school;
            TempPassword = tempPassword;
            TeacherToken = teacherToken;
        }

        public Teacher isTeacherExists(string username, string password)
        {
            DBservices dBservices = new DBservices();
            return dBservices.isTeacherExists(username, password);
            //אם קיים מחזיר מספר מזהה של מחנך, אם לא קיים מחזיר אפס
        }

        public DataTable getTeacher()
        {
            DBservices dBservices = new DBservices();
            return dBservices.getTeacher();
            //מחזיר רשימה של כל המחנכים
        }

        public int getTeacherByMail(string TeacherMail, string username)
        {
            //בדיקה האם קיים מחנך עם המייל הזה - אם קיים מחזיר את המספר המזהה של המחנך, אם לא קיים מחזיר אפס
            DBservices dBservices = new DBservices();
            int teacherID = dBservices.getTeacherByMail(TeacherMail, username);

            //אם המייל קיים מחזיר 1 שמסמל על זה ששונתה הססמה, אם המייל לא קיים מחזיר 0
            if (teacherID == 0)
                return 0;

            // יצירת סיסמה רנדומלית 
            string randomPassword = Membership.GeneratePassword(8, 0);
            //string randomPassword = Guid.NewGuid().ToString("n").Substring(0, 8);

            // שליחת מייל
            bool hasSend = sendMail(TeacherMail, randomPassword);

            //פונקציה שמעדכנת את הסיסמא הרנדומלית בטבלת מחנכים ומכניסה ערך 1 לעמודת סיסמא זמנית
            if (hasSend == true)
                return dBservices.updateTeacherPassword(teacherID, randomPassword, 1);

            // אם שליחת המייל נכשלה
            return 0; 
        }

        public bool sendMail(string TeacherMail, string randomPassword)
        {
            string smtpAddr = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;
            string emailFromAddress = "challengeme.ruppin@gmail.com";
            string password = "oren5555";
            string subject = "challenge me new temporary password";
            string body = "<div dir='rtl'><div>הססמה הזמנית החדשה שלך היא: " + randomPassword + "</div><br /><div>כאשר אתה נכנס אתה תצטרך לשנות את הססמה</div><div>challenge me</div><div>";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(emailFromAddress);
            mail.To.Add(TeacherMail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(smtpAddr, portNumber);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(emailFromAddress, password);
            smtp.EnableSsl = enableSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                smtp.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        } 

        public DataTable getTeacherById(int teacherID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getTeacherById(teacherID); 
        }

        public int GetIsTeacherExistByPhone(string phone)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetIsTeacherExistByPhone(phone);
        }

        public int checkIfTeacherExistByUsername(string username)
        {
            DBservices dBservices = new DBservices();
            return dBservices.checkIfTeacherExistByUsername(username);
        }
        public int postTeacher(Teacher teacher)
        {
            DBservices dbs = new DBservices();
            int teacherID = dbs.postTeacher(teacher);
            AlertSettings a = new AlertSettings();
            return a.postAlertSettings(teacherID);
        }

        public int putNewTeacherPassword(int teacherID, string password)
        {
            DBservices dbs = new DBservices();
            return dbs.updateTeacherPassword(teacherID, password, 0);
        }

        public int putTeacher(Teacher t)
        {
            DBservices dbs = new DBservices();
            return dbs.updateTeacherDetails(t);
        }

        //public int deleteTeacher(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteTeacher(id);
        //}
    }
}