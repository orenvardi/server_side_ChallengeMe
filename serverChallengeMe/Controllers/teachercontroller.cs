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
    public class TeacherController : ApiController
    {
        //GET api/Teacher
        public DataTable Get()
        {
            Teacher teacher = new Teacher();
            return teacher.getTeacher();
        }

        // GET api/Teacher?username={username}&password={password}
        public Teacher Get(string username, string password)
        {
            Teacher teacher = new Teacher();
            return teacher.isTeacherExists(username, password);
        }

        // GET api/Teacher?mail={mail}
        public string Get(string mail)
        {
            Teacher teacher = new Teacher();
            return teacher.getTeacherByMail(mail);
        }

        // GET api/Teacher?teacherID={teacherID}
        public DataTable Get(int teacherID)
        {
            Teacher teacher = new Teacher();
            return teacher.getTeacherById(teacherID);
        }

        // POST api/<controller>
        public int Post(Teacher teacher)
        {
            Teacher t = new Teacher();
            return t.postTeacher(teacher);
        }

        // PUT api/<controller>/5
        public int Put(Teacher t)
        {
            Teacher teacher = new Teacher();
            return teacher.putTeacher(t);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}