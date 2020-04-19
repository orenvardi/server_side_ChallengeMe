using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;

namespace serverChallengeMe.Models
{
    public class StudentFeatures
    {
        public int Answer { get; set; }
        public int StudentID { get; set; }
        public int QuestionID { get; set; }
        public StudentFeatures() { }
        public StudentFeatures(int answer, int studentID, int questionID)
        {
            Answer = answer;
            StudentID = studentID;
            QuestionID = questionID;
        }

        public DataTable getFQBystudentID(int studentID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getFQBystudentID(studentID);
        }

        public DataTable getQuestionsAndAnswers(int studentID)
        {
            DBservices dBservices = new DBservices();
            return dBservices.getQuestionsAndAnswers(studentID);
        }

        public int postStudentFeatures(List<StudentFeatures> StudentFeaturesArr)
        {
            var x = 0;
            DBservices dbs = new DBservices();
            x = dbs.postStudentFeatures(StudentFeaturesArr);
            
            //קריאה לפונקציה שמחשבת את ציוני התלמיד
            int studentID = StudentFeaturesArr[0].StudentID;
            calculateStudentScore(studentID, "post");

            return x;
        }

        public int putStudentFeatures(List<StudentFeatures> StudentFeaturesArr)
        {
            var x = 0;
            DBservices dbs = new DBservices();
            x = dbs.putStudentFeatures(StudentFeaturesArr);
            
            //קריאה לפונקציה שמחשבת את ציוני התלמיד
            int studentID = StudentFeaturesArr[0].StudentID;
            calculateStudentScore(studentID, "put");
            return x;
        }

        //פונקציה שמחשבת ציוני תלמיד ועושה אינסרט לטבלת סטודנט.סקור
        public int calculateStudentScore(int studentID, string command)
        {
            //1. לקרוא לפונקציה שתחזיר מהדאטה בייס את הסכום של כל קטגוריה ואז האחוזים של כל קטגוריה
            DBservices dBservices = new DBservices();
            DataTable studentPercent = dBservices.getStudentPercent(studentID);

            // --start of scallings normalization - from 20-100 to 0-100 
            int min = 20;
            int max = 100;

            double social, emotional, school = 0.0;

            emotional = Convert.ToDouble(studentPercent.Rows[0][2]);
            emotional = (emotional - min) / (max - min) * 100;

            social = Convert.ToDouble(studentPercent.Rows[1][2]) ;
            social = (social - min) / (max - min) * 100;

            school = Convert.ToDouble(studentPercent.Rows[2][2]);
            school = (school - min) / (max - min) * 100;
            // --end of scallings normalization



            //2. עושים אינסרט או פוט של הציונים לטבלת סטודנט סקור
            // בדיקה האם כבר קיימת רשומה לתלמיד
            if (command == "post")
                return dBservices.insertStudentScore(studentID, social, emotional, school);
            else  //command == "put"
                return dBservices.updateStudentScore(studentID, social, emotional, school);
        }
    }
}