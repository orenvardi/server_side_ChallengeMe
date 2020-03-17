using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;

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
            return dBservices.getTeacherByID(username, password);
        }

        public DataTable getTeacher()
        {
            DBservices dBservices = new DBservices();
            return dBservices.getTeacher();
        }

        public int getTeacherByMail(string mail)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getTeacherByMail(mail);
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