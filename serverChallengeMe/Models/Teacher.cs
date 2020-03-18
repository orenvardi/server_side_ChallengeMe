using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
using System.Web.Security;

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

        public Teacher() { }

        public Teacher(int teacherID, string userName, string password, string firstName, string lastName, string phone, string mail, string school)
        {
            TeacherID = teacherID;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Mail = mail;
            School = school;
        }

        public int isTeacherExists(string username, string password)
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

        public string getTeacherByMail(string mail)
        {
            DBservices dBservices = new DBservices();
            var teacherID = dBservices.getTeacherByMail(mail); //אם קיים מחנך עם המייל הזה מחזיר את המספר המזהה של המחנך, אם לא קיים מחזיר אפס
            var randomPassword = "";
            if (teacherID != 0) //במידה שקיים מחנך עם המייל הזה
            {
                randomPassword = Membership.GeneratePassword(8, 1); //פונקציה שיוצרת סיסמא רנדומלית של 8 תווים עם לפחות תו אחד מיוחד
                dBservices.updateTeacherPassword(teacherID, randomPassword); //פונקציה שמעדכנת את הסיסמא הרנדומלית בטבלת מחנכים ומכניסה ערך 1 לעמודת 'סיסמא זמנית'
            }
            return randomPassword;
            //אם המייל קיים מחזיר את הסיסמא הרנדומלית, אם המייל לא קיים מחזיר מחרוזת ריקה
        }

        public int postTeacher(Teacher teacher)
        {
            DBservices dbs = new DBservices();
            return dbs.postTeacher(teacher);
        }

        //public int deleteTeacher(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteTeacher(id);
        //}
    }
}