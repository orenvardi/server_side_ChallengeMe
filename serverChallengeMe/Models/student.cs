using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;


namespace serverChallengeMe.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int ClassID { get; set; }
        public int TeacherID { get; set; }
        public int AvatarID { get; set; }

        public Student() { }

        public Student(int studentID, string userName, string password, string firstName, string lastName, string phone, int classID, int teacherID, int avatarID)
        {
            StudentID = studentID;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            ClassID = classID;
            TeacherID = teacherID;
            AvatarID = avatarID;
        }

        public DataTable getStudents()
        {
            DBservices dBservices = new DBservices();
            return dBservices.getStudents();
        }

        public int postStudent(Student student)
        {
            DBservices dbs = new DBservices();
            return dbs.postStudent(student);
        }

        public int deleteStudent(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.deleteStudent(id);
        }
    }
}