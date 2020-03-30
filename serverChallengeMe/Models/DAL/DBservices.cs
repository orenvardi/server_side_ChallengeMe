using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using serverChallengeMe.Models;

namespace serverChallengeMe.Models.DAL
{
    public class DBservices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBservices()
        {
        }

        //--------------------------------------------------------------------------------------------------
        // 1.  This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        //---------------------------------------------------------------------------------
        // 2.  Create the SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con;              // assign the connection to the command object
            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 
            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure
            return cmd;
        }
        //---------------------------------------------------------------------------------
        // 3.  GET Teachers
        //---------------------------------------------------------------------------------
        public DataTable getTeacher()
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from Teacher;", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }

            catch (Exception ex)
            {
                Console.WriteLine("No rows found.");
                // try to handle the error
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        //---------------------------------------------------------------------------------
        // 4.  GET Teacher By Username and Password
        //---------------------------------------------------------------------------------       
        public Teacher isTeacherExists(string username, string password)
        {
            Teacher t = new Teacher();
            
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select teacherID,tempPassword from Teacher where userName = '" + username + "' AND password = '" + password + "';";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr2 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        t.TeacherID = Convert.ToInt32(dr2["teacherID"]);
                        t.TempPassword = Convert.ToBoolean(dr2["tempPassword"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return t;
        }

        //---------------------------------------------------------------------------------
        // 5.  GET Teacher By mail
        //---------------------------------------------------------------------------------       
        public int getTeacherByMail(string mail)
        {
            int id = 0;
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select teacherID from Teacher where mail = '" + mail + "';";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr2 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        id = Convert.ToInt32(dr2["teacherID"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return id;
        }

        //---------------------------------------------------------------------------------
        // 6.  GET Teacher By ID
        //---------------------------------------------------------------------------------       
        public DataTable getTeacherById(int teacherID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from Teacher where teacherID = '" + teacherID + "';", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }

            catch (Exception ex)
            {
                Console.WriteLine("No rows found.");
                // try to handle the error
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }

        //---------------------------------------------------------------------------------
        // 7.  GET Student Challenge By student ID
        //---------------------------------------------------------------------------------       
        public DataTable getStudentChallenge(int studentID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("SELECT Challenge.challengeID,Challenge.challengeName,Challenge.description ,Category.categoryID,Category.categoryName, StudentChallenge.deadline, StudentChallenge.difficulty, StudentChallenge.status, StudentChallenge.studentID FROM Challenge INNER JOIN StudentChallenge ON Challenge.ChallengeID = StudentChallenge.ChallengeID INNER JOIN Category ON Category.CategoryID = Challenge.CategoryID where StudentChallenge.StudentID = '" + studentID + "';", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }

            catch (Exception ex)
            {
                Console.WriteLine("No rows found.");
                // try to handle the error
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        //---------------------------------------------------------------------------------
        // 8.  GET Challenge
        //---------------------------------------------------------------------------------
        public DataTable getChallenge()
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from Challenge;", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }

            catch (Exception ex)
            {
                Console.WriteLine("No rows found.");
                // try to handle the error
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        //---------------------------------------------------------------------------------
        // 9.  GET Student By User Name And Password
        //---------------------------------------------------------------------------------
        public int getStudentByUserNameAndPassword(string userName, int password)
        {
            int id = 0;
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select studentID from Student where userName = '" + userName + "' AND password = '"+password + "'; ";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr2 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        id = Convert.ToInt32(dr2["studentID"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return id;
        }
        //---------------------------------------------------------------------------------
        // 10.  GET Avatar
        //---------------------------------------------------------------------------------
        public DataTable getAvatar()
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from Avatar;", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }

            catch (Exception ex)
            {
                Console.WriteLine("No rows found.");
                // try to handle the error
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        //---------------------------------------------------------------------------------
        // 11.  POST Teacher
        //---------------------------------------------------------------------------------
        public int postTeacher(Teacher teacher)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            String cStr = BuildInsertCommandTeacher(teacher);      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
              
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        //--------------------------------------------------------------------
        // 12.  Build INSERT Teacher Command
        //--------------------------------------------------------------------
        private String BuildInsertCommandTeacher(Teacher teacher)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("VALUES('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}');", teacher.UserName, teacher.Password, teacher.FirstName, teacher.LastName, teacher.Mail, teacher.Phone, teacher.School);
            String prefix = "INSERT INTO Teacher(userName, password, firstName, lastName, mail, phone, school)";
            command = prefix + sb.ToString();
            return command;
        }
        //---------------------------------------------------------------------------------
        // 11.  POST Student
        //---------------------------------------------------------------------------------
        public int postStudent(Student student)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            String cStr = BuildInsertCommandStudent(student);      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        //--------------------------------------------------------------------
        // 13.  Build INSERT Student Command
        //--------------------------------------------------------------------
        private String BuildInsertCommandStudent(Student student)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("VALUES('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}','{7}');", student.UserName, student.Password, student.FirstName, student.LastName, student.Phone, student.ClassID, student.TeacherID, student.AvatarID);
            String prefix = "INSERT INTO Student(userName, password, firstName, lastName, phone, classID, teacherID, avatarID)";
            command = prefix + sb.ToString();
            return command;
        }
        //---------------------------------------------------------------------------------
        // 14.  POST Challenge
        //---------------------------------------------------------------------------------
        public int postChallenge(Challenge challenge)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            String cStr = BuildInsertCommandChallenge(challenge);      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        //--------------------------------------------------------------------
        // 15.  Build INSERT Challenge Command
        //--------------------------------------------------------------------
        private String BuildInsertCommandChallenge(Challenge challenge)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("VALUES('{0}', '{1}', '{2}');", challenge.ChallemgeName, challenge.Description, challenge.CategoryID);
            String prefix = "INSERT INTO Challenge(challemgeName, description, categoryID)";
            command = prefix + sb.ToString();
            return command;
        }
        //---------------------------------------------------------------------------------
        // 16.  POST Student Challenge
        //---------------------------------------------------------------------------------
        public int postStudentChallenge(StudentChallenge sc)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            String cStr = BuildInsertCommandStudentChallenge(sc);      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        //--------------------------------------------------------------------
        // 17.  Build INSERT Student Challenge Command
        //--------------------------------------------------------------------
        private String BuildInsertCommandStudentChallenge(StudentChallenge sc)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("VALUES('{0}', '{1}', '{2}', '{3}', '{4}');", sc.ChallengeID, sc.StudentID, sc.Difficulty, sc.Deadline, sc.Status);
            String prefix = "INSERT INTO StudentChallenge(challengeID, studentID, difficulty, deadline, status)";
            command = prefix + sb.ToString();
            return command;
        }
        //---------------------------------------------------------------------------------
        // 18.  POST Class
        //---------------------------------------------------------------------------------
        public int postClass(Class c)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            //שימוש בפרמטרים כשיש סיכוי שיהיה מחרוזת עם גרש שיכולה להוות בעיה 
            String cStr = "INSERT INTO Class(className, teacherID) VALUES(@ClassName, " + c.TeacherID + ");";
            cmd = CreateCommand(cStr, con);             
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@ClassName";
            param.Value = c.ClassName;
            cmd.Parameters.Add(param);

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //---------------------------------------------------------------------------------
        // 19.  UPDATE Teacher Password
        //---------------------------------------------------------------------------------
        public int updateTeacherPassword(int teacherID, string randomPassword)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            String cStr = "UPDATE Teacher SET password = '"+randomPassword+"', tempPassword = 1 WHERE TeacherID = "+teacherID+";";  
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        //---------------------------------------------------------------------------------
        // 20.  UPDATE Class Name
        //---------------------------------------------------------------------------------
        public int updateClass(Class c)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = "UPDATE Class SET className = @ClassName  WHERE classID = " + c.ClassID + ";";
            cmd = CreateCommand(cStr, con);             // create the command
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@ClassName";
            param.Value = c.ClassName;
            cmd.Parameters.Add(param);

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                
                throw (ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        //---------------------------------------------------------------------------------
        // 21.  UPDATE Teacher details
        //---------------------------------------------------------------------------------
        public int updateTeacherDetails(Teacher t)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            String cStr = "UPDATE Teacher SET userName  = '" + t.UserName + "', password= '" + t.Password + "', firstName = '" + t.FirstName + "', lastName  = '" + t.LastName + "', mail = '" + t.Mail + "', phone = '" + t.Phone + "', school = '" + t.School + "' WHERE teacherID  = " + t.TeacherID + ";";
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
               
                throw (ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        //---------------------------------------------------------------------------------
        // 22.  UPDATE StudentChallenge details
        //---------------------------------------------------------------------------------
        public int updateStudentChallenge(StudentChallenge sc)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            String cStr = "UPDATE StudentChallenge SET difficulty = '" + sc.Difficulty + "', deadline = '" + sc.Deadline + "', status = '" + sc.Status + "' WHERE challengeID  = " + sc.ChallengeID  + " AND studentID = "+ sc.StudentID + ";";
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        //---------------------------------------------------------------------------------
        // 23.  UPDATE Student details
        //---------------------------------------------------------------------------------
        public int updateStudentDetails(Student s)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            String cStr = "UPDATE Student SET userName = '" + s.UserName + "', password= '" + s.Password + "', firstName = '" + s.FirstName + "', lastName  = '" + s.LastName + "', phone = '" + s.Phone + "', classID = '" + s.ClassID + "', avatarID = '" + s.AvatarID + "' WHERE teacherID  = " + s.TeacherID + " AND studentID = " + s.StudentID + ";";
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        //---------------------------------------------------------------------------------
        // 24.  GET Classes
        //---------------------------------------------------------------------------------
        public DataTable getClass(int teacherID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from Class where teacherID = '"+teacherID+"';", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }

            catch (Exception ex)
            {
                Console.WriteLine("No rows found.");
                // try to handle the error
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        //---------------------------------------------------------------------------------
        // 25.  GET Students by class Id
        //---------------------------------------------------------------------------------
        public DataTable getStudents(int classID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from Student where classID = '" + classID + "';", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }

            catch (Exception ex)
            {
                Console.WriteLine("No rows found.");
                // try to handle the error
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        //---------------------------------------------------------------------------------
        // 26.  GET Student by student Id
        //---------------------------------------------------------------------------------
        public DataTable getStudentById(int studentID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from Student where studentID = '" + studentID + "';", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }

            catch (Exception ex)
            {
                Console.WriteLine("No rows found.");
                // try to handle the error
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        //---------------------------------------------------------------------------------
        // 27.  DELETE Students
        //---------------------------------------------------------------------------------
        public int deleteStudent(int studentID)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            } //DELETE FROM Publishers
            // WHERE City = 'New York'
            String cStr = "DELETE FROM Student WHERE studentID  = '" + studentID + "' ";
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        //---------------------------------------------------------------------------------
        // 28.  DELETE Class
        //---------------------------------------------------------------------------------
        public int deleteClass(int classID)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            } //DELETE FROM Publishers
            // WHERE City = 'New York'
            String cStr = "DELETE FROM Class WHERE classID  = '" + classID + "' ";
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        //---------------------------------------------------------------------------------
        // 29.  DELETE Student Challenge
        //---------------------------------------------------------------------------------
        public int deleteStudentChallenge(int studentID, int challengeID)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            } //DELETE FROM Publishers
            // WHERE City = 'New York'
            String cStr = "DELETE FROM StudentChallenge WHERE studentID  = '" + studentID + "' AND challengeID= '"+ challengeID + "' ";
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
    }
}
