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
        public int isTeacherExists(string username, string password)
        {
            int id = 0;
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select teacherID from Teacher where userName = '" + username + "' AND password = '" + password + "';";
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
        // 7.  POST Teacher
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
                return 0;
                // write to log
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
        // 8.  Build INSERT Teacher Command
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
        // 9.  POST Class
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
                return 0;
                // write to log
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
        // 10.  UPDATE Teacher Password
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
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        //---------------------------------------------------------------------------------
        // 11.  UPDATE Class Name
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
            String cStr = "UPDATE Class SET className = '" + c.ClassName + "' WHERE classID = " + c.ClassID + ";";
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        //---------------------------------------------------------------------------------
        // 12.  UPDATE Teacher details
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
            String cStr = "UPDATE Teacher SET userName  = '" + t.UserName + "' password= '" + t.Password + "' firstName = '" + t.FirstName + "' lastName  = '" + t.LastName + "' mail   = '" + t.Mail + "' phone   = '" + t.Phone + "' scholl   = '" + t.School + "' WHERE teacherID  = " + t.TeacherID + ";";
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        //---------------------------------------------------------------------------------
        // 13.  GET Classes
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
        // 14  GET Students
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
        // 15.  DELETE Students
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
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        //---------------------------------------------------------------------------------
        // 16.  DELETE Class
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
                return 0;
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
