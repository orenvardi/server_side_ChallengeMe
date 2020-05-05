﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
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
        public string Image { get; set; }

        public Student() { }


        public Student(int studentID, string userName, string password, string firstName, string lastName, string phone, int classID, int teacherID, string avatar, string birthDate, string image)
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
            Image = image;
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

        public int GetSuccessCount(int studentID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetSuccessCount(studentID);
        }
        public string GetImageStudent(int studentID)
        {
            DBservices dBservices = new DBservices();
            // לוקחים את נתיב התמונה ששמור כסטרינג בטבלה בדאטה בייס
            string imagePath = dBservices.GetImageStudent(studentID);
            // ממירים את נתיב התמונה מסטרינג למערך של ביטים
            byte[] imageArray = System.IO.File.ReadAllBytes(imagePath);
            // ממירים את מערך הביטים לבייס 64
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            // מחזירים את התמונה לצד לקוח כבייס 64
            return base64ImageRepresentation;
           
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
        public int putStudentImage(Student student)
        {
            // התמונה מתקבלת מהצד לקוח כבייס 64, שומרים אותה במשתנה
            string base64StringData = student.Image; // Your base 64 string data

            //remove everything before the first /
            string type = base64StringData.Substring(base64StringData.IndexOf("/") + 1);

            // remove everything after the first /
            type = type.Substring(0, type.IndexOf(";"));

            // מגדירים ששם הקובץ יהיה המספר המזהה של האתגר עם הסיומת המתאימה
            string fileName = student.StudentID.ToString() + "sImg." + type;

            // נתיב התמונה נלקח מהמחלקה הסטטית שלנו בתוספת שם הקובץ
            string imagePath = PathOfImage.path + fileName;

            // חותכים את התחלת הסטרינג כי זה מיותר
            string cleandata = base64StringData.Replace("data:image/" + type + ";base64,", "");

            // עושים המרה מבייס 64 למערך של ביטים
            byte[] data = System.Convert.FromBase64String(cleandata);

            MemoryStream ms = new MemoryStream(data);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            // שומרים את התמונה בנתיב שהגדרנו
            img.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            // שומרים בטבלה בדאטה בייס את נתיב התמונה
            DBservices dbs = new DBservices();
            return dbs.putStudentImage(imagePath, student.StudentID);
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