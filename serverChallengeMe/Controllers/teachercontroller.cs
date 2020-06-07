using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using serverChallengeMe.Models;
using System.Web.Http.Cors;

namespace serverChallengeMe.Controllers
{
    [EnableCors("*", "*", "GET, POST, PUT, DELETE")]

    public class TeacherController : ApiController
    {
        //GET api/Teacher
        public DataTable Get()
        {
            Teacher teacher = new Teacher();
            return teacher.getTeacher();
        }

        [HttpGet]
        [Route("api/Teacher/GetTeachersByInstitution")]
        //GET api/Teacher
        public DataTable GetTeachersByInstitution(int teacherID)
        {
            Teacher teacher = new Teacher();
            return teacher.GetTeachersByInstitution(teacherID);
        }

        // GET api/Teacher?username={username}&password={password}
        public Teacher Get(string username, string password)
        {
            Teacher teacher = new Teacher();
            return teacher.isTeacherExists(username, password);
        }

        // GET api/Teacher?mail={mail}&username={username}
        public int GetByMail(string mail, string username)
        {
            Teacher teacher = new Teacher();
            return teacher.getTeacherByMail(mail, username);
        }

        // GET api/Teacher?teacherID={teacherID}
        public DataTable GetByTeacherID(int teacherID)
        {
            Teacher teacher = new Teacher();
            return teacher.getTeacherById(teacherID);
        }

        // GET api/Teacher?usernameNewTeacher={username}
        public int GetIsTeacherExistByUsername(string usernameNewTeacher)
        {
            Teacher teacher = new Teacher();
            return teacher.checkIfTeacherExistByUsername(usernameNewTeacher);
        }

        // GET api/Teacher?phone={phone}
        public int GetIsTeacherExistByPhone(string phone)
        {
            Teacher teacher = new Teacher();
            return teacher.GetIsTeacherExistByPhone(phone);
        }

        [HttpGet]
        [Route("api/Teacher/getTeacherToken")]
        // GET api/Teacher/getTeacherToken
        public string getTeacherToken(int teacherID)
        {
            Teacher t = new Teacher();
            return t.getTeacherToken(teacherID);
        }

        // POST api/<controller>
        public int Post(Teacher teacher)
        {
            Teacher t = new Teacher();
            return t.postTeacher(teacher);
        }

        [HttpPut]
        [Route("api/Teacher/teacherToken")]
        // GET api/Teacher/teacherToken
        public int PutTeacherToken(Teacher teacher)
        {
            Teacher t = new Teacher();
            return t.PutTeacherToken(teacher);
        }

        // PUT api/Teacher?teacherID={teacherID}&password={password}
        public int PutTeacherPassword(int teacherID, string password)
        {
            Teacher teacher = new Teacher();
            return teacher.putNewTeacherPassword(teacherID, password);
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