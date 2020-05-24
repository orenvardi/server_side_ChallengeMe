using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using serverChallengeMe.Models;

namespace serverChallengeMe
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        // Code that runs on application startup
        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            // Dynamically create new timer
            System.Timers.Timer timScheduledTask = new System.Timers.Timer();

            // Timer interval is set in miliseconds (1000 miliseconds = 1 second),
            // In this case, we'll run a task every minute
            // 1000milisecs * 60sec * 1440min = 24hours
            timScheduledTask.Interval = 1000 * 60 * 1440;

            timScheduledTask.Enabled = true;

            // Add handler for Elapsed event
            timScheduledTask.Elapsed +=
            new System.Timers.ElapsedEventHandler(timScheduledTask_Elapsed);
        }

        void timScheduledTask_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Execute some task
            Alert alert = new Alert();
            alert.passDeadlineAlert();
            alert.idleStudentAlert();
            alert.preDeadlineAlert();
            alert.idleStudentsAlert();
            alert.preDeadlineStudentsAlert();
            alert.passDeadlineStudentsAlert();
        }
    }
}