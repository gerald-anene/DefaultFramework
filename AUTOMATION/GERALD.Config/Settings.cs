using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GERALD.Config
{
    public class Settings
    {

        public static string JiraUrl;
        public static string JiraUser;
        public static string JiraPassword;

        public static string JiraProject;
        public static string JiraVersion;
        public static string JiraCycle;

        public static bool UpdateExecutionStatus;

        public static int PassExecutionStatus;
        public static int FailExecutionStatus;
        public static int unexecutedStatus;

        public static string IeDriverPath;
        public static string ChromeDriverPath;
        public static string BrowserType;
        public static int ImplicitTimeoutSeconds;

        public static string asdaUrl;

        static Settings()
        {
            log4net.Config.XmlConfigurator.Configure();

            var appSettings = ConfigurationManager.AppSettings;

            IeDriverPath = appSettings["ie.driver.path"];
            ChromeDriverPath = appSettings["chrome.driver.path"];
            BrowserType = appSettings["browser.type"];
            ImplicitTimeoutSeconds = Convert.ToInt16(appSettings["implicit.timeout.seconds"]);


            JiraUrl = appSettings["jira.base.url"];
            JiraUser = appSettings["jira.user"];
            JiraPassword = appSettings["jira.password"];

            JiraProject = appSettings["jira.project.name"];
            JiraVersion = appSettings["jira.version.name"];
            JiraCycle = appSettings["Jira.cycle.name"];

            asdaUrl = appSettings["asda.url"];
            

            UpdateExecutionStatus = Convert.ToBoolean(appSettings["jira.update.execution.status"]);

            PassExecutionStatus = Convert.ToInt16(appSettings["pass.execution.status"]);
            FailExecutionStatus = Convert.ToInt16(appSettings["fail.execution.status"]);
            unexecutedStatus = Convert.ToInt16(appSettings["unexecuted.execution.status"]);

        }

    }
}
