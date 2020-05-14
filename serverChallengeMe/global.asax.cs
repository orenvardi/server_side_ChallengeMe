using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace serverChallengeMe
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private const string DummyCacheItemKey = "GagaGuguGigi";

        private const string DummyPageUrl = "http://localhost:54573";

        private void HitPage()
        {
            WebClient client = new WebClient();
            client.DownloadData(DummyPageUrl);
        }

        protected void Application_Start(Object sender, EventArgs e)
        {
            //Application["Title"] = "Builder.com Sample";
            RegisterCacheEntry();
        }

        private bool RegisterCacheEntry()
        {
            if (null != HttpContext.Current.Cache[DummyCacheItemKey]) return false;

            HttpContext.Current.Cache.Add(DummyCacheItemKey, "Test", null,
                DateTime.MaxValue, TimeSpan.FromMinutes(2),
                CacheItemPriority.Normal,
                new CacheItemRemovedCallback( CacheItemRemovedCallback ));

            return true;
        }

        public void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            Debug.WriteLine("Cache item callback: " + DateTime.Now.ToString());

            HitPage();

            // Do the service works

            DoWork();
        }

        private void DoWork()
        {
            Debug.WriteLine("Begin DoWork...");
            Debug.WriteLine("Running as: " +
                  WindowsIdentity.GetCurrent().Name);

            DoSomeFileWritingStuff();
            DoSomeDatabaseOperation();
            //DoSomeWebserviceCall();
            //DoSomeMSMQStuff();
            DoSomeEmailSendStuff();

            Debug.WriteLine("End DoWork...");
        }

        private void DoSomeEmailSendStuff()
        {
            string smtpAddr = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;
            string emailFromAddress = "challengeme.ruppin@gmail.com";
            string password = "oren5555";
            string subject = "challenge me new temporary password";
            string body = "<div dir='rtl'><div>הססמה הזמנית החדשה שלך היא: </div><br /><div>כאשר אתה נכנס אתה תצטרך לשנות את הססמה</div><div>challenge me</div><div>";
            string TeacherMail = "orenvardi1@gmail.com";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(emailFromAddress);
            mail.To.Add(TeacherMail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(smtpAddr, portNumber);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(emailFromAddress, password);
            smtp.EnableSsl = enableSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                smtp.Send(mail);
            }
            catch (Exception x)
            {
                Debug.WriteLine(x);
            }
        }

        private void DoSomeFileWritingStuff()
        {
            Debug.WriteLine("Writing to file...");

            try
            {
                using (StreamWriter writer =
                 new StreamWriter(@"c:\temp\Cachecallback.txt", true))
                {
                    writer.WriteLine("Cache Callback: {0}", DateTime.Now);
                    writer.Close();
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine(x);
            }

            Debug.WriteLine("File write successful");
        }

        private void DoSomeDatabaseOperation()
        {
            Debug.WriteLine("Connecting to database...");

            string cStr = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cStr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT" +
                       " INTO ASPNETServiceLog VALUES" +
                       " (@Message, @DateTime)", con))
                {
                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 1024).Value =
                                       "Hi I'm the ASP NET Service";
                    cmd.Parameters.Add("@DateTime", SqlDbType.DateTime).Value =
                                       DateTime.Now;

                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }

            Debug.WriteLine("Database connection successful");
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            // If the dummy page is hit, then it means we want to add another item

            // in cache

            if (HttpContext.Current.Request.Url.ToString() == DummyPageUrl)
            {
                // Add the item in cache and when succesful, do the work.

                RegisterCacheEntry();
            }
        }

        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    GlobalConfiguration.Configure(WebApiConfig.Register);
        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);
        //}

        //protected void Session_Start(Object sender, EventArgs e)
        //{
        //    Session["startValue"] = 0;
        //}

        //protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        //{
        //    // Extract the forms authentication cookie
        //    string cookieName = FormsAuthentication.FormsCookieName;
        //    HttpCookie authCookie = Context.Request.Cookies[cookieName];
        //    if (null == authCookie)
        //    {
        //        // There is no authentication cookie.
        //        return;
        //    }
        //    FormsAuthenticationTicket authTicket = null;
        //    try
        //    {
        //        authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception details (omitted for simplicity)
        //        return;
        //    }
        //    if (null == authTicket)
        //    {
        //        // Cookie failed to decrypt.
        //        return;
        //    }
        //    // When the ticket was created, the UserData property was assigned
        //    // a pipe delimited string of role names.
        //    string[2] roles;
        //    roles[0] = "One";
        //    roles[1] = "Two";
        //    // Create an Identity object
        //    FormsIdentity id = new FormsIdentity(authTicket);
        //    // This principal will flow throughout the request.
        //    GenericPrincipal principal = new GenericPrincipal(id, roles);
        //    // Attach the new principal object to the current HttpContext object
        //    Context.User = principal;
        //}

        //protected void Application_Error(Object sender, EventArgs e)
        //{
        //    Response.Write("Error encountered.");
        //}
    }
}
