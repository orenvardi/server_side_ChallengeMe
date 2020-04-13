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
            for (int i = 0; i < StudentFeaturesArr.Count; i++)
            {
                x = dbs.postStudentFeatures(StudentFeaturesArr[i]);
            }
            //קריאה לפונקציה שמחשבת את ציוני התלמיד
            int studentID = StudentFeaturesArr[0].StudentID;
            calculateStudentScore(studentID);
            return x;
        }

        //פונקציה שמחשבת ציוני תלמיד ועושה אינסרט לטבלת סטודנטסקור
        public void calculateStudentScore(int studentID)
        {
            //1. לקרוא לפונקציה שתחזיר מהדאטה בייס את הסכום של כל קטגוריה ואז האחוזים של כל קטגוריה
            //לכתוב פונקציה בDBservices
            DBservices dBservices = new DBservices();
            DataTable studentPercent = dBservices.getStudentPercent(studentID);
            //2. עושים אינסרט של הציונים לטבלת סטודנט סקור כולל ציון ממוע של שלושתם
            //לכתוב פונקציה בDBservisec
            return dBservices.insertStudentScore(studentPercent);
        }

        public int putStudentFeatures(List<StudentFeatures> StudentFeaturesArr)
        {
            var x = 0;
            DBservices dbs = new DBservices();
            for (int i = 0; i < StudentFeaturesArr.Count; i++)
            {
                x = dbs.putStudentFeatures(StudentFeaturesArr[i]);
            }
            return x;
        }
    }
}