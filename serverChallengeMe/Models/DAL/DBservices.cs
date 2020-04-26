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
                da = new SqlDataAdapter("SELECT Challenge.*, StudentChallenge.* FROM Challenge INNER JOIN StudentChallenge ON Challenge.ChallengeID = StudentChallenge.ChallengeID where StudentChallenge.StudentID = " + studentID + ";", con);
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
        public int getStudentByPhoneAndPassword(string phone, int password)
        {
            int id = 0;
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select studentID from Student where phone = '" + phone + "' AND password = '"+password + "'; ";
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
        // .  GET Student By Phone
        //---------------------------------------------------------------------------------
        public int getStudentByPhone(string phone)
        {
            int id = 0;
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select studentID from Student where phone = '" + phone  + "'; ";
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
            sb.AppendFormat("VALUES('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}','{7}');", teacher.UserName, teacher.Password, teacher.FirstName, teacher.LastName, teacher.Mail, teacher.Phone, teacher.School,'0');
            String prefix = "INSERT INTO Teacher(userName, password, firstName, lastName, mail, phone, school,tempPassword)";
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
                //int numEffected = cmd.ExecuteNonQuery(); // execute the command
                int newID = Convert.ToInt32(cmd.ExecuteScalar()); //return the output from the query
                return newID;
          
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
            sb.AppendFormat("VALUES('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}');", student.Password, student.FirstName, student.LastName, student.Phone, student.ClassID, student.TeacherID, student.BirthDate);
            String prefix = "INSERT INTO Student(password, firstName, lastName, phone, classID, teacherID, birthDate) output INSERTED.studentID ";
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
                int newID = Convert.ToInt32(cmd.ExecuteScalar()); //return the output from the query
                return newID;
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
            sb.AppendFormat("VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}');", challenge.ChallengeName, challenge.Difficulty, challenge.SocialMin, challenge.SocialMax, challenge.EmotionalMin, challenge.EmotionalMax, challenge.SchoolMin, challenge.SchoolMax, challenge.IsPrivate);
            String prefix = "INSERT INTO Challenge(challengeName, difficulty, socialMin, socialMax, emotionalMin,  emotionalMax, schoolMin, schoolMax, isPrivate) output INSERTED.challengeID ";
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
        public int updateTeacherPassword(int teacherID, string password, int tempPassword)
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
            String cStr = "UPDATE Teacher SET password = '"+password+"', tempPassword = "+ tempPassword + " WHERE TeacherID = "+teacherID+";";  
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
            string timeStamp = (sc.Status == "0" ? "', timeStamp = null " : "' ");
            String cStr = "UPDATE StudentChallenge SET difficulty = '" + sc.Difficulty +
                "', deadline = '" + sc.Deadline + "', status = '" + sc.Status + timeStamp +
                " WHERE challengeID  = " + sc.ChallengeID  + " AND studentID = "+ sc.StudentID + ";";
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
            String cStr = "UPDATE Student SET password= '" + s.Password + "', firstName = '" + s.FirstName + "', lastName  = '" + s.LastName + "', phone = '" + s.Phone + "', birthDate = '" + s.BirthDate +"' WHERE studentID = " + s.StudentID + ";";
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
        //---------------------------------------------------------------------------------
        // 30.  get checkIfTeacherExistByUsername
        //---------------------------------------------------------------------------------
        public int checkIfTeacherExistByUsername(string userName)
        {
            
            Teacher t = new Teacher();

            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select teacherID from Teacher where userName = '" + userName+ "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr2 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows)
                {
                  
                        return 1;
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
            return 0;
        }
        //---------------------------------------------------------------------------------
        // 31.  get Challenge By Name
        //---------------------------------------------------------------------------------
        public DataTable getChallengeByName(string challengeName)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from Challenge where isPrivate = 'false' AND challengeName = '" + challengeName + "';", con);
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
        // 32.  GET Tag
        //---------------------------------------------------------------------------------
        public DataTable getTag()
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from Tag;", con);
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
        // 33.  GET Challenge by student Id
        //---------------------------------------------------------------------------------
        public DataTable getChallengeByID(int challengeID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from Challenge where challengeID = '" + challengeID + "';", con);
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
        // 34.  GET Features Question
        //---------------------------------------------------------------------------------
        public DataTable getFeaturesQuestion()
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from featuresQuestion ;", con);
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
        // 35.  GET Features Question By studentID
        //---------------------------------------------------------------------------------
        public DataTable getFQBystudentID(int studentID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from studentFeatures where studentID = '" + studentID + "';", con);
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
        // 35.  GET Features Question AND Answers By studentID
        //---------------------------------------------------------------------------------
        public DataTable getQuestionsAndAnswers(int studentID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                string cStr = "select FQ.*, SF.studentID, SF.answer, C.categoryName from featuresQuestion FQ left join(select* from studentFeatures " +
                    "where studentID = " + studentID + ") as sf on sf.questionID = FQ.questionID join category C on FQ.categoryID = C.categoryID ORDER BY FQ.categoryID";
                da = new SqlDataAdapter(cStr, con);
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
        // 37.  GET Challenge Tag
        //---------------------------------------------------------------------------------
        public DataTable getCT(int[] tagIDArr)
        {
            SqlConnection con = null;
            try
            {
                string str = "ChallengeTag.TagID = " + tagIDArr[0];
                if (tagIDArr.Length > 1)
                {
                    for (int i = 1; i < tagIDArr.Length; i++)
                        str += " OR ChallengeTag.TagID = " + tagIDArr[i];
                }

                con = connect("DBConnectionString");
                da = new SqlDataAdapter("SELECT DISTINCT Challenge.* FROM Challenge INNER JOIN ChallengeTag ON Challenge.ChallengeID = ChallengeTag.ChallengeID INNER JOIN Tag ON Tag.TagID =ChallengeTag.TagID where " + str , con);
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
        // 38.  POST ChallengeTag
        //---------------------------------------------------------------------------------
        public int postChallengeTag(ChallengeTag challengeTag)
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
            StringBuilder cStr = new StringBuilder();
            cStr.AppendFormat("INSERT INTO ChallengeTag(challengeID, tagID) VALUES({0},{1});", challengeTag.ChallengeID, challengeTag.TagID);
            cmd = CreateCommand(cStr.ToString(), con);             // create the command
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
        // 39.  POST StudentFeatures
        //---------------------------------------------------------------------------------
        public int postStudentFeatures(List<StudentFeatures> StudentFeaturesArr)
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

            string str = "";
            for (int i = 0; i < StudentFeaturesArr.Count; i++)
            {
                str += " INSERT INTO StudentFeatures(studentID, questionID, answer) VALUES(" + StudentFeaturesArr[i].StudentID + "," + StudentFeaturesArr[i].QuestionID + "," + StudentFeaturesArr[i].Answer + "); ";
            }

            cmd = CreateCommand(str.ToString(), con);      // create the command
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
        // 39.  PUT StudentFeatures
        //---------------------------------------------------------------------------------
        public int putStudentFeatures(List<StudentFeatures> StudentFeaturesArr)
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

            string str = "";
            for (int i = 0; i < StudentFeaturesArr.Count; i++)
            {
                str += " UPDATE studentFeatures SET answer = " + StudentFeaturesArr[i].Answer + " WHERE studentID = " + StudentFeaturesArr[i].StudentID + " AND questionID = " + StudentFeaturesArr[i].QuestionID + "; ";
            }

            cmd = CreateCommand(str.ToString(), con);      // create the command
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
        // 40.  GET Student Percent
        //---------------------------------------------------------------------------------
        public DataTable getStudentPercent(int studentID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                string str = "select FQ.categoryID, sum(SF.answer) as 'sum', sum(SF.answer) * 2 as 'percent' " +
                    " from studentFeatures SF join featuresQuestion FQ on SF.questionID = FQ.questionID" +
                    " where SF.studentID = " + studentID + " group by FQ.categoryID";
                da = new SqlDataAdapter(str, con);
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
        // 41.  INSERT StudentFeatures
        //---------------------------------------------------------------------------------
        public int insertStudentScore(int studentID, double social, double emotional, double school)
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
            StringBuilder cStr = new StringBuilder();
            cStr.AppendFormat("INSERT INTO studentScore(studentID, social, school, emotional) VALUES({0},{1},{2},{3});", studentID, social, school, emotional);
            cmd = CreateCommand(cStr.ToString(), con);             // create the command
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
        // 42.  UPDATE StudentFeatures
        //---------------------------------------------------------------------------------
        public int updateStudentScore(int studentID, double social, double emotional, double school)
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
            StringBuilder cStr = new StringBuilder();
            cStr.AppendFormat("UPDATE studentScore SET social = {0}, school = {1}, emotional = {2} WHERE studentID = {3};", social, school, emotional, studentID);
            cmd = CreateCommand(cStr.ToString(), con);             // create the command
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
        // 43.  GET StudentScore By studentID
        //---------------------------------------------------------------------------------       
        public StudentScore getStudentScore(int studentID)
        {
            StudentScore studentScore = new StudentScore();

            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select * from StudentScore where studentID = '" + studentID + "';";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr2 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        studentScore.StudentID = Convert.ToInt32(dr2["studentID"]);
                        studentScore.Social = Convert.ToDouble(dr2["social"]);
                        studentScore.Emotional = Convert.ToDouble(dr2["emotional"]);
                        studentScore.School = Convert.ToDouble(dr2["school"]);
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
            return studentScore;
        }
        //---------------------------------------------------------------------------------
        // 44.  GET SmartElementOffer 
        //---------------------------------------------------------------------------------
        public DataTable findSmartElementOffer(StudentScore studentScore)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                string str = "select TOP 30 c.* from " +
                    " (select a.challengeID, max(a.popularity) as 'popularity' " +
                    " from (select C.challengeID, count(C.challengeID) as 'popularity' " +
                    " from challenge C left join studentChallenge sc on c.challengeID = sc.challengeID " +
                    " where(" + studentScore.Social + " BETWEEN  C.socialMin AND  C.socialMax) "+
                    " AND (" + studentScore.Emotional + " BETWEEN  C.emotionalMin AND  C.emotionalMax) " +
                    " AND(" + studentScore.School + " BETWEEN  C.schoolMin AND  C.schoolMax) " +
                    " GROUP BY C.challengeID " +
                    " union all " +
                    " select Sch.challengeID, count(Sch.challengeID) as 'popularity' " +
                    " from StudentScore Sscore inner join studentChallenge Sch on Sscore.studentID = Sch.studentID " +
                    " where Sch.status = 1 and Sch.timeStamp < Sch.deadline " +
                    " GROUP BY Sch.challengeID, Sscore.social, Sscore.emotional, Sscore.school " +
                    " having ABS(" + studentScore.Social + " - Sscore.social) < 10 "+
                    " AND ABS(" + studentScore.Emotional + " - Sscore.emotional) < 10 "+
                    " AND  ABS(" + studentScore.School + " - Sscore.school) < 10 " +
                    " ) as a " +
                    " group by a.challengeID) as b " +
                    " inner join challenge c on c.challengeID = b.challengeID " +
                    " order by b.popularity DESC";

                da = new SqlDataAdapter(str, con);
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
        // 45.  get Challenges Without Student Challenges
        //---------------------------------------------------------------------------------
        public DataTable getChallengesWithoutStudentChallenges(int studentID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                string str = "select * from Challenge where isPrivate = 'false' AND challengeID not in (select challengeID from studentChallenge where studentID = " + studentID + ");";
                da = new SqlDataAdapter(str, con);
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
        // 46.  get Students With Message
        //---------------------------------------------------------------------------------
        public List<int> getStudentsWithMessage(int teacherID)
        {
            List<int> studentIDList = new List<int>();
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select studentID from message where teacherID = " + teacherID + " group by studentID order By max(messageDate) DESC;";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr2 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        studentIDList.Add(Convert.ToInt32(dr2["studentID"]));
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
            return studentIDList;
        }
        //---------------------------------------------------------------------------------
        // 47.  get Number of Massage For Teache From specific Stu That Not Read
        //---------------------------------------------------------------------------------
        public int getMessageTfromSnotRead(int teacherID, int studentID)
        {
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
                String cStr = "select COUNT(messageID) from message where (MesgRead = 'false' or MesgRead is null) AND messageByTeacher = 'false' AND studentID = " + studentID + " AND teacherID = " + teacherID + "; ";
                cmd = CreateCommand(cStr, con);             // create the command
                try
                {
                    int unReadCount = Convert.ToInt32(cmd.ExecuteScalar()); //return the output from the query
                    return unReadCount;
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
        }
        //---------------------------------------------------------------------------------
        // 48. get All Message for spasific teacher and student
        //---------------------------------------------------------------------------------
        public DataTable getAllMessage(int teacherID, int studentID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                string str = "select * from message M join student S on M.studentID = S.studentID where M.studentID = " + studentID + ";";
                da = new SqlDataAdapter(str, con);
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
        // 49.  UPDATE If Message Read 
        //---------------------------------------------------------------------------------
        public int updateMessage(int teacherID, int studentID)
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
            String cStr = "UPDATE Message SET MesgRead = 'true' where messageByTeacher='false' AND studentID = " + studentID + " AND teacherID = " + teacherID + ";";
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
        // 50.  INSERT Message
        //---------------------------------------------------------------------------------
        public int postMessage(Message message)
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
            String cStr = BuildInsertCommandMessage(message);      // helper method to build the insert string
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
        // 51.  Build INSERT Message Command
        //--------------------------------------------------------------------
        private String BuildInsertCommandMessage(Message message)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("VALUES('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}');", message.TeacherID, message.StudentID, message.MessageTitle, message.MessageText, message.MessageDate, message.MessageTime, message.MessageByTeacher);
            String prefix = "INSERT INTO Message(teacherID, studentID, messageTitle, messageText, messageDate, messageTime, messageByTeacher)";
            command = prefix + sb.ToString();
            return command;
        }
        //---------------------------------------------------------------------------------
        // 52.  GET Students by TeacherID
        //---------------------------------------------------------------------------------
        public DataTable GetStudentsByTeacherID(int teacherID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from Student where teacherID = '" + teacherID + "';", con);
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
        // 53.  get UnRead Message Count for teacher
        //---------------------------------------------------------------------------------
        public int getUnReadMessageCount(int teacherID)
        {
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
                String cStr = "select COUNT(messageID) from message where (MesgRead = 'false' or MesgRead is null) AND messageByTeacher = 'false' AND teacherID = " + teacherID + "; ";
                cmd = CreateCommand(cStr, con);             // create the command
                try
                {
                    int unReadCount = Convert.ToInt32(cmd.ExecuteScalar()); //return the output from the query
                    return unReadCount;
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
        }
        //---------------------------------------------------------------------------------
        // 54.  UPDATE StudentChallenge status
        //---------------------------------------------------------------------------------
        public int updateStatus(int challengeID, int studentID, string status)
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
            String cStr = "UPDATE StudentChallenge SET status = '" + status + "' WHERE challengeID  = " + challengeID + " AND studentID = " + studentID + ";";
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
        // 55.  get Student UnRead Message Count for
        //---------------------------------------------------------------------------------
        public int getStudentUnReadMessageCount(int studentID)
        {
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
                String cStr = "select COUNT(messageID) from message where (MesgRead = 'false' or MesgRead is null) AND messageByTeacher = 'true' AND studentID = " + studentID + "; ";
                cmd = CreateCommand(cStr, con);             // create the command
                try
                {
                    int unReadCount = Convert.ToInt32(cmd.ExecuteScalar()); //return the output from the query
                    return unReadCount;
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
        }
        //---------------------------------------------------------------------------------
        // 56.  UPDATE If Message Read 
        //---------------------------------------------------------------------------------
        public int updateStudentMessage(int studentID)
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
            String cStr = "UPDATE Message SET MesgRead = 'true' where messageByTeacher='true' AND studentID = " + studentID+ ";";
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
        // 47.  ???????????????? get Number of Messege For Students That Not Read
        //---------------------------------------------------------------------------------
        public DataTable getNumOfMessageNotReadForStudents(int studentID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                string str = "select COUNT(messageID) from message where studentID = " + studentID + " AND MesgRead=1;";
                da = new SqlDataAdapter(str, con);
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
        // 48. ??????????????????????? get Number of Alert For Students That Not Read
        //---------------------------------------------------------------------------------
        public DataTable getNumOfAlertNotReadForStudents(int studentID)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                string str = "select COUNT(messageID) from alert where studentID = " + studentID + " AND alertRead=1;";
                da = new SqlDataAdapter(str, con);
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
// 49. get Name of students by id
//---------------------------------------------------------------------------------

public DataTable getStudentNameById(int studentID)
{
    SqlConnection con = null;
    try
    {
        con = connect("DBConnectionString");
        da = new SqlDataAdapter("select firstName, lastName, avatarID from Student where studentID =" + studentID + ";", con);
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



    }
}