using System;
using System.Text;
using System.IO;

namespace CheckinSvc
{
    class WebService : iContract
    {
        CheckinSvc c = new CheckinSvc();
        //public CRUD crud = new CRUD();
        public Config config = new Config();

        public bool isTest = false;

        public WebService(Config config) {
            this.config = config;
        }
        public WebService() { }

        public string TestMethod()
        {
            return "SUCCESS";
        }


        public Stream GetData(string patientid)
        {

            c.WriteToLog("In GetData");
            //if (!isTest)
            //{
            //    System.ServiceModel.Web.WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            //    string authorization = System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.Headers[System.Net.HttpRequestHeader.Authorization];

            //    // authentication stuff
            //    if (!isAuthorized(authorization))
            //        return StringToStream("Unauthorized");
            //}

            try
            {

                // end
                //DataTable dt = crud.GetDatafromLocalDatabase(start, end, since);
                //c.WriteToLog("GetData datatable size is " + dt.Rows.Count.ToString());
                //string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
                //c.WriteToLog("GetData bp 3a json length is " + json.Length.ToString());

                return StringToStream("test");

                //Failure during Database Call to GetData Log entry string is too long. A string written to the event log cannot exceed 32766 characters.
            }
            catch (Exception e)
            {
                c.WriteToLog("Failure during Call to GetData " + e.Message);
                return StringToStream("");
            }
        }

        public Stream StringToStream(string s)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(s));

        }

        public string StreamToString(Stream ms)
        {
            try
            {
                using (ms)
                {
                    var sr = new StreamReader(ms);
                    var myStr = sr.ReadToEnd();
                    sr.Dispose();
                    return myStr;
                };
            }
            catch (Exception e)
            {
                c.WriteToLog("StreamToString error - " + e.Message);
                return "";

            }

        }



        //public Stream PatientCheckin(string apptid)
        //{

        //    try
        //    {
        //        string authorization = System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.Headers[System.Net.HttpRequestHeader.Authorization];

        //        if (!isAuthorized(authorization))
        //            return StringToStream("Unauthorized");

        //        c.WriteToLog("PatientCheckin apptid is " + apptid);
        //        crud.PatientCheckin(apptid);
        //        c.WriteToLog("Past Patient Checkin");
        //        c.GetDataFromCPS();  // refresh data
        //        c.WriteToLog("Past GetDataFromCPS");
        //        string status = crud.VerifyCheckin(apptid);
        //        c.WriteToLog("Past GetApptStatusbyApptID");
        //        return StringToStream(status);
        //    }
        //    catch (Exception e) {
        //        c.WriteToLog("PatientCheckin error - " + e.Message);
        //        return StringToStream(e.Message);
        //    }
        //}

        //public Stream UndoPatientCheckin(string apptid) {

        //    try {
        //        string authorization = System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.Headers[System.Net.HttpRequestHeader.Authorization];

        //        if (!isAuthorized(authorization))
        //            return StringToStream("Unauthorized");

        //        c.WriteToLog("UndoPatientCheckin apptid is " + apptid);
        //        crud.UndoPatientCheckin(apptid);
        //        c.WriteToLog("Past Patient Checkin");
        //        c.GetDataFromCPS();  // refresh data
        //        c.WriteToLog("Past GetDataFromCPS");
        //        string status = crud.VerifyCheckin(apptid);
        //        c.WriteToLog("Past GetApptStatusbyApptID");
        //        return StringToStream(status);
        //    } catch (Exception e) {
        //        c.WriteToLog("PatientCheckin error - " + e.Message);
        //        return StringToStream(e.Message);
        //    }
        //}
        //public Stream StringToStream(string s) {
        //     return new MemoryStream(Encoding.UTF8.GetBytes(s));

        //}

        //public string StreamToString(Stream ms) {
        //    try {
        //        using (ms) {
        //            var sr = new StreamReader(ms);
        //            var myStr = sr.ReadToEnd();
        //            sr.Dispose();
        //            return myStr;
        //        };
        //    } catch (Exception e) {
        //        c.WriteToLog("StreamToString error - " + e.Message);
        //        return "";

        //    }

        //}


        //public Stream PatientDemos(string PatientProfileId, ApptData data) {

        //    // a post call
        //    if (!isTest) {
        //        System.ServiceModel.Web.WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
        //        string authorization = System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.Headers[System.Net.HttpRequestHeader.Authorization];

        //        // authentication stuff
        //        if (!isAuthorized(authorization))
        //            return StringToStream("Unauthorized");
        //    }
        //    try {
        //        if (data != null) {

        //            c.WriteToLog("first name from data is " + data.PatientFirst);
        //            //if (data.PatientProfileId == null)
        //            //    data.PatientProfileId = PatientProfileId;
        //            if (data.PatientProfileId == null)
        //                c.WriteToLog("No PatientProfileId found");
        //            c.WriteToLog("PatientProfileId id from payload is " + data.PatientProfileId);
        //            crud.UpdateDemographics(data);// post demographics
        //            c.WriteToLog("past update demographics");
        //            return StringToStream("");
        //        } else {
        //            c.WriteToLog("data are null");
        //            return StringToStream("data are null");
        //        }
        //    } catch (Exception e) {
        //        c.WriteToLog("Failure during PatientCheckinWithDemos " + e.Message);
        //        return StringToStream("Failure during PatientCheckinWithDemos " + e.Message);
        //    }
        //}

        //public bool isAuthorized(string authorization) {


        //    if (authorization.IndexOf("Basic ") == 0) {
        //        string authstring = authorization.Substring(6);
        //        byte[] decryptedauth = Convert.FromBase64String(authstring);
        //        string decryptedstr = System.Text.Encoding.Default.GetString(decryptedauth);

        //        string user = decryptedstr.Substring(0, decryptedstr.IndexOf(":"));
        //        string pw = decryptedstr.Substring(decryptedstr.IndexOf(":") + 1);

        //        c.WriteToLog("user and pw  is " + user + "   " + pw);

        //        //Config config = new Config();
        //        if (config == null || config.IntegrationID == null || config.IntegrationID.Length <= 0)
        //            config.ReadConfig();
        //        c.WriteToLog("config.IntegrationID is " + config.IntegrationID);

        //        if (user != config.LocationID)
        //            return false;
        //        if (pw != config.IntegrationID)
        //            return false;

        //        else return true;

        //    } else return false;

        //}

        //public Stream GetDataEncrypt(string start, string end, string since) {

        //    c.WriteToLog("In GetDataEncrypt");
        //    if (!isTest) {
        //        System.ServiceModel.Web.WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
        //        string authorization = System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.Headers[System.Net.HttpRequestHeader.Authorization];

        //        // authentication stuff
        //        if (!isAuthorized(authorization))
        //            return StringToStream("Unauthorized");
        //    }

        //    try {

        //        // end
        //        DataTable dt = crud.GetDatafromLocalDatabase(start, end, since);
        //        c.WriteToLog("GetDataEncrypt datatable size is " + dt.Rows.Count.ToString());
        //        string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
        //        c.WriteToLog("GetDataEncrypt bp 3a json length is " + json.Length.ToString());

        //        //if (config == null || config.IntegrationID == null || config.IntegrationID.Length <= 0)
        //        //    config.ReadConfig();
        //        c.WriteToLog("GetDataEncrypt bp 4b Integration ID is " + config.IntegrationID);

        //        string encryptjson = Crypto.EncryptStringAES(json,config.IntegrationID);
        //        //File.WriteAllText(@"c:\temp\encrypt.txt", encryptjson);
        //        return StringToStream(encryptjson);

        //        //Failure during Database Call to GetData Log entry string is too long. A string written to the event log cannot exceed 32766 characters.
        //    } catch (Exception e) {
        //        c.WriteToLog("Failure during Call to GetDataEncrypt " + e.Message);
        //        return StringToStream("");
        //    }
        //}


        //public Stream AddAppointment(ApptData data) {
        //    try {
        //        if (data != null) {
        //            c.WriteToLog("AddAppointment PatientProfileId id from payload is " + data.PatientProfileId);
        //            c.WriteToLog("AddAppointment AppointmentsId id from payload is " + data.AppointmentsId);
        //            crud.AddAppointment(data.PatientProfileId); // check in the patient
        //            c.WriteToLog("past AddAppointment");
        //            c.GetDataFromCPS();  // refresh data
        //            c.WriteToLog("past Get data from Cps");
        //            return StringToStream("");
        //        } else {
        //            c.WriteToLog("data are null using PPID 1");
        //            crud.AddAppointment("1"); // check in the patient
        //            c.WriteToLog("past AddAppointment");
        //            c.GetDataFromCPS();  // refresh data
        //            c.WriteToLog("past Get data from Cps");
        //            return StringToStream("");
        //        }
        //    } catch (Exception e) {
        //        c.WriteToLog("Failure during AddAppointment " + e.Message);
        //        return StringToStream("Failure during AddAppointment " + e.Message);
        //    }
        //}

        //public Stream DeleteAppointment(ApptData data) {
        //    try {
        //        if (data != null) {
        //            c.WriteToLog("DeleteAppointment PatientProfileId id from payload is " + data.PatientProfileId);
        //            c.WriteToLog("DeleteAppointment AppointmentsId id from payload is " + data.AppointmentsId);
        //            crud.DeleteAppointment(data.AppointmentsId); // check in the patient
        //            c.WriteToLog("past DeleteAppointment");
        //            c.GetDataFromCPS();  // refresh data
        //            c.WriteToLog("past Get data from Cps");
        //            return StringToStream("");
        //        } else {
        //            c.WriteToLog("data are null");
        //            return StringToStream("data are null");
        //        }
        //    } catch (Exception e) {
        //        c.WriteToLog("Failure during DeleteAppointment " + e.Message);
        //        return StringToStream("Failure during DeleteAppointment " + e.Message);
        //    }
        //}

        //public Stream GetConfig() {

        //    try {
        //        string json = JsonConvert.SerializeObject(Config.ConfigToArray(), Newtonsoft.Json.Formatting.Indented);
        //        c.WriteToLog("Config called - JSON is " + json);
        //        return StringToStream(json);
        //    } catch (Exception e) {
        //        c.WriteToLog("Failure during GetConfig " + e.Message);
        //        return StringToStream("Failure during GetConfig " + e.Message);
        //    }
        //}



        //public Stream GetPicture(string PatientProfileId) {
        //    string authorization = System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.Headers[System.Net.HttpRequestHeader.Authorization];

        //    if (!isAuthorized(authorization))
        //        return StringToStream("Unauthorized");

        //    c.WriteToLog("In GetPicture");
        //    byte[] s = crud.GetPicture(PatientProfileId);

        //    string output = ByteToBase64(s);

        //    c.WriteToLog("Past GetPicture");
        //    return StringToStream(output);
        //}

        //private string ByteToBase64(byte[] s) {
        //    return Convert.ToBase64String(s);
        //}

        //private byte[] Base64toByte(string s) {
        //    return Convert.FromBase64String(s);
        //}

        //public Stream SetPicture(string PatientProfileId, Stream data) {
        //    try {
        //        string authorization = System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.Headers[System.Net.HttpRequestHeader.Authorization];

        //        if (!isAuthorized(authorization))
        //            return StringToStream("Unauthorized");

        //        c.WriteToLog("PatientProfileId is " + PatientProfileId);

        //        string picturestr = StreamToString(data).Replace("\r", "").Replace("\n", "");

        //        c.WriteToLog("data length is " + picturestr.Length.ToString() + " data is " + picturestr.Substring(0,30));

        //        c.WriteToLog("SetPicture bp 1 ");
        //        byte[] picture = Base64toByte(picturestr);
        //        c.WriteToLog("SetPicture bp 2 ");
        //        crud.SetPicture(PatientProfileId, picture);
        //        c.WriteToLog("SetPicture bp 3 ");
        //        return StringToStream("SUCCESS");
        //    } catch (Exception e) {
        //        c.WriteToLog("Failure during SetPicture " + e.Message);
        //        return StringToStream("Failure during SetPicture " + e.Message);
        //    }
        //}

        //public Stream SetDocument(string PatientProfileId, string Name, Stream dataStream) {
        //    try {
        //        //string authorization = System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.Headers[System.Net.HttpRequestHeader.Authorization];

        //        //if (!isAuthorized(authorization))
        //        //    return StringToStream("Unauthorized");

        //        c.WriteToLog("PatientProfileId is " + PatientProfileId);
        //        c.WriteToLog("Name is " + Name);

        //        string datastr = StreamToString(dataStream).Replace("\r", "").Replace("\n", "");

        //        c.WriteToLog("data length is " + datastr.Length.ToString() + " data is " + datastr.Substring(0, 30));

        //        c.WriteToLog("SetDocument bp 1 ");
        //        byte[] databyte = Base64toByte(datastr);
        //        c.WriteToLog("SetDocument bp 2 ");
        //        crud.SetDocument(PatientProfileId, Name, databyte);
        //        c.WriteToLog("SetDocument bp 3 ");
        //        return StringToStream("SUCCESS");
        //    } catch (Exception e) {
        //        c.WriteToLog("Failure during SetDocument " + e.Message);
        //        return StringToStream("Failure during SetDocument " + e.Message);
        //    }
        //}

    }
}
