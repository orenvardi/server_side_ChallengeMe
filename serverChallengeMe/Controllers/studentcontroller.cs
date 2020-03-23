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
        //GET api/Student
        public DataTable Get(int classID)
        {
            Student student = new Student();
            return student.getStudents(classID);
        }

    //// GET api/Student?studentID={studentID}
    //public DataTable Get(int studentID)
    //{
    //    Student student = new Student();
    //    return student.getStudentById(studentID);
    //}

    // POST api/<controller>
    public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public int Delete(int studentID)
        {
            Student stu = new Student();
            return stu.deleteStudent(studentID);
        }
    }
}