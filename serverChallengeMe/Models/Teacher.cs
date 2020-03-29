using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
using System.Web.Security;

using System.Net;
using System.Net.Mail;

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

        public Teacher() { }

        public Teacher(int teacherID, string userName, string password, string firstName, string lastName, string phone, string mail, string school,bool tempPassword)
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

        public int getTeacherByMail(string mail)
        {
            DBservices dBservices = new DBservices();
            var teacherID = dBservices.getTeacherByMail(mail); //אם קיים מחנך עם המייל הזה מחזיר את המספר המזהה של המחנך, אם לא קיים מחזיר אפס
            var randomPassword = "";
            if (teacherID != 0) //במידה שקיים מחנך עם המייל הזה
            {
                randomPassword = Membership.GeneratePassword(8, 1); //פונקציה שיוצרת סיסמא רנדומלית של 8 תווים עם לפחות תו אחד מיוחד
                dBservices.updateTeacherPassword(teacherID, randomPassword); //פונקציה שמעדכנת את הסיסמא הרנדומלית בטבלת מחנכים ומכניסה ערך 1 לעמודת 'סיסמא זמנית'
                try { 
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("challenge.me555555@gmail.com");
                message.To.Add(new MailAddress(mail));
                message.Subject = "challenge me new temporary password";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "<div><div>הססמה הזמנית החדשה שלך היא: "+ randomPassword + "</div><div>כאשר אתה נכנס אתה תצטרך לשנות את הססמה</div><div>challenge me</div><div>";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("challenge.me555555@gmail.com", "oren5555");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                    return 1;
            } catch (Exception e) { throw e; }
        }
    
            return 0;
            //אם המייל קיים מחזיר 1 שמסמל על זה ששונתה הססמה, אם המייל לא קיים מחזיר 0
        }

        public DataTable getTeacherById(int teacherID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getTeacherById(teacherID); 
        }

        public int postTeacher(Teacher teacher)
        {
            DBservices dbs = new DBservices();
            return dbs.postTeacher(teacher);
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