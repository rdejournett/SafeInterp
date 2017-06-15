using CheckinSvc;
using InterpCheckSvc;
using System;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Windows.Forms;

namespace WindowsService1
{
    public partial class Main : ServiceBase
    {
        WebServiceHost host = null;
        Config config = null;
        Helper h = new Helper();
        //private static System.Timers.Timer aTimer;

        public Main()
        {
            InitializeComponent();
            if (CreateEventLogSource() == false)
                return;

            //try
            //{
            //    System.Diagnostics.EventLog.SourceExists("SAFEInterpSvc");
            //}
            //catch (Exception e)
            //{
            //    System.Diagnostics.EventLog.CreateEventSource(
            //        "SAFEInterpSvc", "SAFEInterpSvcLog");
            //}
            //finally
            //{
                eventLog1.Source = Application.ProductName;
                eventLog1.Log = Application.ProductName + "Log";

            //}
        }

        protected override void OnStart(string[] args)
        {

            StartWebService();
            config = new Config();
            config.ReadConfig();

            //aTimer = new System.Timers.Timer(1000 * 60);  // run every minute
            //aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            //aTimer.Enabled = true;
            WriteToLog("OnTimedEvent registered - polling freq is " + aTimer.Interval.ToString());
            OnTimedEvent(null, null); // do the first tick

        }

        protected override void OnStop()
        {
        }

        public void WriteToLog(string s)
        {
            eventLog1.WriteEntry(s);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // tick

            //GetDataFromCPS();

        }

        private void StartWebService()
        {
            try
            {
                if (config == null)
                    config = new Config();
                config.ReadConfig();
                string port = config.ServicePort;
                h.WriteToLog("Listening on port" + port);

                // THESE LINES FOR HTTPS
                //Uri httpsUrl = new Uri("https://0.0.0.0:" + port + "/");
                //host = new WebServiceHost(typeof(WebService), httpsUrl);
                //WebHttpBinding binding = new WebHttpBinding();
                //binding.Security.Mode = WebHttpSecurityMode.Transport;
                //binding.MaxReceivedMessageSize = 1024 * 1024;  // 1 MB

                // this is for basic auth
                // binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                //System.ServiceModel.Description.ServiceCredentials sc = new ServiceCredentials();
                //sc.UserNameAuthentication.UserNamePasswordValidationMode = System.ServiceModel.Security.UserNamePasswordValidationMode.Custom;
                //customUserNameValidator = new CustomUserNameValidator();
                //sc.UserNameAuthentication.CustomUserNamePasswordValidator = customUserNameValidator;

                // THIS IS FOR NORMAL HTTP
                Uri httpUrl = new Uri("http://localhost:" + port + "/");
                host = new WebServiceHost(typeof(WebService), httpUrl);
                var binding = new WebHttpBinding(); // NetTcpBinding();
                binding.MaxReceivedMessageSize = int.MaxValue;
                host.AddServiceEndpoint(typeof(iContract), binding, "");

                ServiceDebugBehavior stp = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                stp.HttpHelpPageEnabled = true;  // probably remove for prod
                stp.HttpsHelpPageEnabled = true;
                stp.IncludeExceptionDetailInFaults = true;

                host.Open();
            }
            catch (Exception e)
            {
                eventLog1.WriteEntry("Error in StartWebService" + e.Message);
            }

        }

        // ---- Create Event Log Source ---------------------------------
        //
        // returns True if is it created or already exists.
        //
        // Only administrators can create event logs.

        static public bool CreateEventLogSource()
        {
            System.Diagnostics.Debug.WriteLine("CreateEventLogSource....");

            try
            {
                // this call is looking for this RegKey: HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Services\EventLog\Application\<app Name>
                if (EventLog.SourceExists(Application.ProductName))
                {
                    System.Diagnostics.Debug.WriteLine("Log exists, returning true.");
                    return true;
                }
            }
            catch (System.Security.SecurityException)
            {
                // it could not find the EventLog Source and we are not admin so this is thrown 
                // when it tries to search the Security Log. 
                // We know it isn't there so ignore this exception
            }

            System.Diagnostics.Debug.WriteLine("EventLog Source doesn't exist....try to create it...");

            if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                System.Diagnostics.Debug.WriteLine("Running as Admin....trying to create....");
                try
                {
                    EventLog.CreateEventSource(Application.ProductName, "Application");

                    System.Diagnostics.Debug.WriteLine("Successfully create EventLogSource");
                    return true;
                }
                catch (Exception Exp)
                {
                    MessageBox.Show("Error Creating EventLog Source: " + Exp.Message, Application.ProductName);
                    return false;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Need to restart with admin roles");

                ProcessStartInfo AdminProcess = new ProcessStartInfo();
                AdminProcess.UseShellExecute = true;
                AdminProcess.WorkingDirectory = Environment.CurrentDirectory;
                AdminProcess.FileName = Application.ExecutablePath;
                AdminProcess.Verb = "runas";

                try
                {
                    Process.Start(AdminProcess);
                    return false;
                }
                catch
                {
                    MessageBox.Show("The EventLog source was NOT created", Application.ProductName);
                    // The user refused to allow privileges elevation.
                    return false;
                }
            }
        }
    }
}
