﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;

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

        public DataTable getClass()
        {
            DBservices dBservices = new DBservices();
            return dBservices.getClass();
        }

        public int postClass(Class c)
        {
            DBservices dbs = new DBservices();
            return dbs.postClass(c);
        }

        public int deleteClass(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.deleteClass(id);
        }
    }
}