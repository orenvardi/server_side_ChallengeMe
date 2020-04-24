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
    public class StudentController : ApiController
    {
        //GET api/Student?classID={classID}
        public DataTable GetByClassID(int classID)
        {
            Student student = new Student();
            return student.getStudents(classID);
        }

        //GET api/Student?teacherID={teacherID}
        public DataTable GetStudentsByTeacherID(int teacherID)
        {
            Student student = new Student();
            return student.GetStudentsByTeacherID(teacherID);
        }

        // GET api/Student?studentID={studentID}
        public DataTable GetByStudentID(int studentID)
        {
            Student student = new Student();
            return student.getStudentById(studentID);
        }

        // GET api/Student?studentPhone={Phone}
        public int GetByStudentPhone(string phone)
        {
            Student student = new Student();
            return student.getStudentByPhone(phone);
        }

        // GET api/
        public int GetByPhoneAndPassword(string phone, int password)
        {
            Student student = new Student();
            return student.getStudentByPhoneAndPassword(phone, password);
        }

        // POST api/<controller>
        public DataTable Post(Student student)
        {
            Student s = new Student();
            return s.postStudent(student);

        }

        public int Put(Student student)
        {
            Student s = new Student();
            return s.putStudent(student);
        }

        // DELETE api/<controller>/5
        public int Delete(int studentID)
        {
            Student stu = new Student();
            return stu.deleteStudent(studentID);
        }
    }
}