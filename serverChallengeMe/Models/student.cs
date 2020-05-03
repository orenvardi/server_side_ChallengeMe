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
        public string Avatar { get; set; }
        public string BirthDate { get; set; }

        public Student() { }


        public Student(int studentID, string userName, string password, string firstName, string lastName, string phone, int classID, int teacherID, string avatar, string birthDate)
        {
            StudentID = studentID;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            ClassID = classID;
            TeacherID = teacherID;
            Avatar = avatar;
            BirthDate = birthDate;
        }

        public DataTable getStudents(int classID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getStudents(classID);
            //מחזיר רשימה של כל התלמידים של כיתה
        }

        public DataTable GetStudentsByTeacherID(int teacherID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetStudentsByTeacherID(teacherID);
        }

        public DataTable getStudentById(int studentID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getStudentById(studentID);
        }
        public DataTable getStudenNametById (int studentID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getStudentNameById(studentID);
        }

        public DataTable getStudentByPhoneAndPassword(string phone, string password)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getStudentByPhoneAndPassword(phone, password);
        }

        public int getStudentByPhone(string phone)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getStudentByPhone(phone);
        }

        public DataTable postStudent(Student student)
        {
            DBservices dbs = new DBservices();
            int studentID = dbs.postStudent(student);
            return dbs.getStudentById(studentID);
        }

        public int putStudent(Student student)
        {
            DBservices dbs = new DBservices();
            return dbs.updateStudentDetails(student);
        }
    
        public int putAvatar(int studentID, string avatar)
        {
            DBservices dbs = new DBservices();
            return dbs.putAvatar(studentID, avatar);
        }

        public int deleteStudent(int studentID)
        {
            DBservices dbs = new DBservices();
            return dbs.deleteStudent(studentID);
        }
    }
}