using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
//123456
namespace serverChallengeMe.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int TeacherID { get; set; }

        public Class() { }

        public Class(int classID, string className, int teacherID)
        {
            ClassID = classID;
            ClassName = className;
            TeacherID = teacherID;
        }

        public DataTable getClass(int teacherID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getClass(teacherID);
            //מחזיר רשימה של כל הכיתות של מחנך
        }

        public int postClass(Class c)
        {
            DBservices dbs = new DBservices();
            return dbs.postClass(c);
        }

        public int putClass(Class c)
        {
            DBservices dbs = new DBservices();
            return dbs.updateClass(c);
        }

        public int deleteClass(int classID)
        {
            DBservices dbs = new DBservices();
            return dbs.deleteClass(classID);
        }
    }
}