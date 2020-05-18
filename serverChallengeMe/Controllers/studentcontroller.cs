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

        //GET api/Student?teacherID={teacherID}&name={name}
        public DataTable GetStudentsByName(int teacherID, string name)
        {
            Student student = new Student();
            return student.searchStudentsByName(teacherID, name);
        }

        // GET api/Student?studentID={studentID}
        public DataTable GetByStudentID(int studentID)
        {
            Student student = new Student();
            return student.getStudentById(studentID);
        }

        // GET api/Student?studentIDGivesName={studentID}
        public DataTable GetNameByStudentID(int studentIDGivesName)
        {
            Student student = new Student();
            return student.getStudentNametById(studentIDGivesName);
        }

        // GET api/Student?studentPhone={Phone}
        public int GetStudentIdByPhone(string phone)
        {
            Student student = new Student();
            return student.GetStudentIdByPhone(phone);
        }

        [HttpGet]
        [Route("api/Student/GetStudentByPhone")]
        // GET api/Student/GetStudentByPhone?phone={phone}
        public Student GetStudentByPhone(string phone)
        {
            Student student = new Student();
            return student.GetStudentByPhone(phone);
        }

        // GET api/
        public DataTable GetByPhoneAndPassword(string phone, string password)
        {
            Student student = new Student();
            return student.getStudentByPhoneAndPassword(phone, password);
        }

        [HttpGet]
        [Route("api/Student/SuccessCount")]
        // GET api/Student?studentID={studentID}
        public List<int> GetSuccessCount(int studentID)
        {
            Student student = new Student();
            return student.GetSuccessCount(studentID);
        }

        [HttpGet]
        [Route("api/Student/ImageStudent")]
        // GET api/Student?studentID={studentID}
        public string GetImageStudent(int studentID)
        {
            Student student = new Student();
            return student.GetImageStudent(studentID);
        }

        [HttpGet]
        [Route("api/Student/getStudentToken")]
        // GET api/Student/getStudentToken
        public string getStudentToken(int studentID)
        {
            Student s = new Student();
            return s.getStudentToken(studentID);
        }

        [HttpPost]
        [Route("api/Student/studentToken")]
        // GET api/Student/studentToken
        public int PostStudentToken(Student student)
        {
            Student s = new Student();
            return s.PostStudentToken(student);
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
        [HttpPut]
        [Route("api/Student/AddImgStudent")]
        // image + studentID
        public int Put2(Student student)
        {
            Student s = new Student();
            return s.putStudentImage(student);
        }

        // מעדכן את האווטאר שהתלמיד בחר בטבלת תלמיד
        public int Put(int studentID, string avatar)
        {
            Student s = new Student();
            return s.putAvatar(studentID, avatar);
        }

        // DELETE api/<controller>/5
        public int Delete(int studentID)
        {
            Student stu = new Student();
            return stu.deleteStudent(studentID);
        }
    }
}