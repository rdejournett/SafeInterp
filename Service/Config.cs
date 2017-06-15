using System.Data;
using System.Configuration;
using Newtonsoft.Json;

namespace InterpCheckSvc
{
    class Config {
        // Database stuff
        public string DatabaseServer = "";
        public string DatabaseInstance = "";

        public string DatabaseName = "";
        public string DatabaseUser = "";
        public string DatabasePassword = "";

        public string ServicePort = "";

        [JsonProperty("PracticeID")]
        public string PracticeID = "";
        [JsonProperty("GroupID")]
        public string GroupID = "";
        [JsonProperty("IntegrationID")]
        public string IntegrationID = "";
        [JsonProperty("LocationID")]
        public string LocationID = "";

        public void ReadConfig() {

            ServicePort =       ConfigurationManager.AppSettings["ServicePort"];
            DatabaseServer =    ConfigurationManager.AppSettings["DatabaseServer"];
            DatabaseInstance =  ConfigurationManager.AppSettings["DatabaseInstance"];
            DatabaseName =      ConfigurationManager.AppSettings["DatabaseName"];
            DatabaseUser =      ConfigurationManager.AppSettings["DatabaseUser"];
            string encrypt =    ConfigurationManager.AppSettings["DatabasePassword"];

            //DatabasePassword =  Crypto.DecryptStringAES(encrypt, "*08oate");
            DatabasePassword = encrypt;

            IntegrationID =     ConfigurationManager.AppSettings["IntegrationID"];
            GroupID =           ConfigurationManager.AppSettings["GroupID"];
            LocationID =        ConfigurationManager.AppSettings["LocationID"];
            PracticeID =        ConfigurationManager.AppSettings["PracticeID"];





        } // ReadConfig

        public static DataTable ConfigToDatatable() {
            DataTable dt = new DataTable();
            dt.Columns.Add("Key", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            dt.Rows.Add("GroupID", ConfigurationManager.AppSettings["GroupID"]);
            dt.Rows.Add("IntegrationID", ConfigurationManager.AppSettings["IntegrationID"]);
            dt.Rows.Add("LocationID", ConfigurationManager.AppSettings["LocationID"]);

            return dt;

        }

        public static string[,] ConfigToArray() {
            string[,] a = new string[,]
            	{
        	        {"GroupID", ConfigurationManager.AppSettings["GroupID"]},
                    {"IntegrationID", ConfigurationManager.AppSettings["IntegrationID"]},
                    {"LocationID", ConfigurationManager.AppSettings["LocationID"]}
	            };
            return a;

        }
    } // class
}
