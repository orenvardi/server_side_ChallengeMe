using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using serverChallengeMe.Models;

// test sync to github

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
        // 2.  GET Teacher By ID
        //---------------------------------------------------------------------------------       
        public int getTeacherByID(string username, string password)
        {
            var id = 0;
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select teacherID from Teacher where userName = '" + username + "' AND password = '" + password + "';";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                cmd.

            }
            catch (Exception ex)
            {
                // write to log
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
        // 2.  Create the SqlCommand
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        // 2.  Create the SqlCommand
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        // 2.  Create the SqlCommand
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        // 2.  Create the SqlCommand
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        // 2.  Create the SqlCommand
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        // 2.  Create the SqlCommand
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        // 2.  Create the SqlCommand
        //---------------------------------------------------------------------------------
    }
}