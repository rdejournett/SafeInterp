using NHapi.Base.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Timers;

namespace InterpConsoleApp
{
    class Program
    {

        static WebServiceHost host = null;
        //static ClientInfo info = null;
        //static AppToken token = null;
        static Config config = null;
        static Helper h = new InterpConsoleApp.Helper();
        //CustomUserNameValidator customUserNameValidator = null;

        private static System.Timers.Timer aTimer;

        static void Main(string[] args)
        {
            // get list of lab results
            //List<LookupData> dataList = ParseORU();
            //// interpret the list
            //ReturnDataList returnDataList = InterpretData(dataList);
            // send the data back


            StartWebService();

            config = new Config();
            config.ReadConfig();
            

            h.WriteToLog("Server is from config " + config.DatabaseServer);
            h.WriteToLog("IntegrationID is from config " + config.IntegrationID);
            h.WriteToLog("LocationID is from config " + config.LocationID);


            //authentication
            //info = GetClientInfo();
            //WriteToLog("ClientID is " + info.clientId);
            //token = GetAppToken(info);
            //WriteToLog("Token is " + token.accessToken);
            // use the refresh token to decrypt inbound demographics and encrypt outbound stuff
            // use the access token to authorize - compare with what we have vs what is in the inbound
            // end authentication
            aTimer = new System.Timers.Timer(1000 * 60);  // run every minute
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Enabled = true;
            h.WriteToLog("OnTimedEvent registered - polling freq is " + aTimer.Interval.ToString());
            OnTimedEvent(null, null); // do the first tick
            Console.ReadLine(); // service is up and waiting
        }



        static private string GetExampleORU()
        {
            string oru = @"MSH|^~\&|QLS|TME^05D0642827^CLIA||90046003|20170511112837.000-0700||ORU^R01^ORU_R01|80000000000001033201|P|2.5.1|1||AL|NE|||||LRI_NG_RN_Profile^^2.16.840.1.113883.9.20^ISO
PID|1||SAFE.11111111^^^^PT~8567100960284467^^^^AN||SMITH^JILL^^^^^L||19581228|M|||1111 MILL ST^^ROSWELL^GA^30076^USA
NTE|1|L|FASTING:NO
ORC|RE|9^QUEST_TME_90046003|WH350986T^QUEST_TME||CM|||||||1497875025^KAWESCH^GARY^^^^^^^^^^NPI|||||||||SAFE IPC,A PROFESSIONAL CORP.^^^^^^^^^90046003|7904 SANTA MONICA BLVD STE 300^MAIL000^WEST HOLLYWOOD^CA^90046-5170^^O^^USA|^^^^^310^5792778||||||||11363^CHLAMYDIA/GC RNA,TMA^99QDI^7000011363^^UNITCODE
OBR|1|9^QUEST_TME_90046003|WH350986T^QUEST_TME|17134^SPEC ID NOTIFICATION^99QDI|||20170510133600.000-0700||||G|||||1497875025^KAWESCH^GARY^^^^^^^^^^NPI||||||20170511112837.000-0700|||F||||9&QUEST_TME_90046003^WH350986T&QUEST_TME|||||||||||||||||||||11363^CHLAMYDIA/GC RNA,TMA^99QDI^7000011363^^UNITCODE
OBX|1||86006556^COMMENT:^99QDI^8251-1^Service Cmnt XXX-Imp^LN|1|||||||F|||20170511112837.000-0700|||||20170511081746.000-0700||||QUEST DIAGNOSTICS-WEST HILLS^^^^^^FI^CLIA^^05D0642827|8401 FALLBROOK AVENUE^^WEST HILLS^CA^91304-3226|1366479099^TERRAZAS^ENRIQUE^^MD^^^^^^^^NPI^EN
NTE|1|L|Specimen labels must include two forms of patient 
NTE|2|L|ID. Only one unique identifier was present on the 
NTE|3|L|sample(s). The testing you requested will be 
NTE|4|L|processed; however, going forward please provide 
NTE|5|L|two identifiers as required by the College of 
NTE|6|L|American Pathologists (CAP).
SPM|1|||USPEC^Source, Unspecified^HL70487|||||||||||||20170510133600.000-0700|20170511081747.000-0700
ORC|RE|9^QUEST_TME_90046003|WH350986T^QUEST_TME||CM|||||||1497875025^KAWESCH^GARY^^^^^^^^^^NPI|||||||||SAFE IPC,A PROFESSIONAL CORP.^^^^^^^^^90046003|7904 SANTA MONICA BLVD STE 300^MAIL000^WEST HOLLYWOOD^CA^90046-5170^^O^^USA|^^^^^310^5792778
OBR|2|9^QUEST_TME_90046003|WH350986T^QUEST_TME|11363^CHLAMYDIA/GC RNA,TMA^99QDI|||20170510133600.000-0700|||||||||1497875025^KAWESCH^GARY^^^^^^^^^^NPI||||||20170511112837.000-0700|||F
NTE|1|L|This test was performed using the APTIMA COMBO2 Assay
NTE|2|L|(Gen-Probe Inc.).
NTE|3|L| 
NTE|4|L|The analytical performance characteristics of this 
NTE|5|L|assay, when used to test SurePath specimens have
NTE|6|L|been determined by Quest Diagnostics.
NTE|7|L|  
OBX|1|ST|70043800^CHLAMYDIA TRACHOMATIS RNA, TMA^99QDI^43304-5^C trach rRNA XXX Ql PCR^LN|1|DETECTED||NOT DETECTED|A|||F|||20170511112837.000-0700|||||20170511082903.000-0700||||QUEST DIAGNOSTICS-WEST HILLS^^^^^^FI^CLIA^^05D0642827|8401 FALLBROOK AVENUE^^WEST HILLS^CA^91304-3226|1366479099^TERRAZAS^ENRIQUE^^MD^^^^^^^^NPI^EN
NTE|1|L| 
NTE|2|L|A positive CT or NG Nucleic Acid Amplification Test
NTE|3|L|(NAAT) result should be interpreted in conjunction
NTE|4|L|with other laboratory and clinical data available to
NTE|5|L|the clinician. If clinically indicated, further 
NTE|6|L|testing can be performed on the same sample using
NTE|7|L|an alternate molecular target. To order alternate
NTE|8|L|target test use 15031 (C. trachomatis) or 
NTE|9|L|15033 (N. gonorrhoeae).
NTE|10|L| 
OBX|2|ST|70043900^NEISSERIA GONORRHOEAE RNA, TMA^99QDI^43305-2^N gonorrhoea rRNA XXX Ql PCR^LN|1|DETECTED||NOT DETECTED|A|||F|||20170511112837.000-0700|||||20170511082903.000-0700||||QUEST DIAGNOSTICS-WEST HILLS^^^^^^FI^CLIA^^05D0642827|8401 FALLBROOK AVENUE^^WEST HILLS^CA^91304-3226|1366479099^TERRAZAS^ENRIQUE^^MD^^^^^^^^NPI^EN
NTE|1|L| 
NTE|2|L|A positive CT or NG Nucleic Acid Amplification Test
NTE|3|L|(NAAT) result should be interpreted in conjunction
NTE|4|L|with other laboratory and clinical data available to
NTE|5|L|the clinician. If clinically indicated, further 
NTE|6|L|testing can be performed on the same sample using
NTE|7|L|an alternate molecular target. To order alternate
NTE|8|L|target test use 15031 (C. trachomatis) or 
NTE|9|L|15033 (N. gonorrhoeae).
NTE|10|L| 
SPM|1|01^01||USPEC^Source, Unspecified^HL70487|||||||||||||20170510133600.000-0700|20170511081747.000-0700
ORC|RE|9^QUEST_TME_90046003|WH350986T^QUEST_TME||CM|||||||1497875025^KAWESCH^GARY^^^^^^^^^^NPI|||||||||SAFE IPC,A PROFESSIONAL CORP.^^^^^^^^^90046003|7904 SANTA MONICA BLVD STE 300^MAIL000^WEST HOLLYWOOD^CA^90046-5170^^O^^USA|^^^^^310^5792778||||||||11363^CHLAMYDIA/GC RNA,TMA^99QDI^7000011363^^UNITCODE
OBR|3|9^QUEST_TME_90046003|WH350986T^QUEST_TME|ClinicalPDFReport1^Clinical PDF Report WH350986T-1^99QDI|||20170510133600.000-0700|||||||||1497875025^KAWESCH^GARY^^^^^^^^^^NPI||||||20170511112837.000-0700|||F
OBX|1|ED|ClinicalPDFReport1^Clinical PDF Report WH350986T-1^99QDI||TME^IM^^Base64^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||||||F||||||||||||QUEST DIAGNOSTICS-WEST HILLS^^^^^^FI^CLIA^^05D0642827|8401 FALLBROOK AVENUE^^WEST HILLS^CA^91304-3226|1366479099^TERRAZAS^ENRIQUE^^MD^^^^^^^^NPI^EN
";
            return oru;
        }

        static private List<LookupData> ParseORU()
        {
            PipeParser pipeParser = new PipeParser();
            try
            {
                string ORUstring = GetExampleORU();
                NHapi.Base.Model.IMessage message = pipeParser.Parse(ORUstring);
                List<LookupData> dataList = new List<LookupData>();
                if (message is NHapi.Model.V251.Message.ORU_R01)
                {
                    NHapi.Model.V251.Message.ORU_R01 ORU = (NHapi.Model.V251.Message.ORU_R01) message;

                    foreach( var ptresults in ORU.PATIENT_RESULTs)
                    {
                        foreach (var order in ptresults.ORDER_OBSERVATIONs) { 
                            //string orderID = order.OBR.FillerOrderNumber.EntityIdentifier.Value;
                            //string orderName = order.OBR.UniversalServiceIdentifier.Identifier.Value;
                            // loop on OBX
                            foreach (var result in order.OBSERVATIONs)
                            {
                                LookupData data = new InterpConsoleApp.LookupData();
                                // get lab facility
                                if (ORU.MSH.SendingApplication.NamespaceID.Value == "QLS") data.FacilityName = "Quest";
                                data.OrderID = order.OBR.FillerOrderNumber.EntityIdentifier.Value;
                                data.LabCode = order.OBR.UniversalServiceIdentifier.Identifier.Value; // OBR4.1
                                data.LabName = order.OBR.UniversalServiceIdentifier.Text.Value; // OBR4.2
                                data.ResultCode = result.OBX.ObservationIdentifier.Identifier.Value; // OBX3.1
                                data.ResultName = result.OBX.ObservationIdentifier.Text.Value; // OBX3.2
                                NHapi.Base.Model.IType obsData = result.OBX.GetObservationValue(0).Data; // OBX5
                                if (obsData.TypeName == "ST" )
                                {
                                    data.ResultValue = obsData.ToString();
                                }
                                dataList.Add(data);

                            }
                        }
                    }
                }
                return dataList;
            }
            catch (Exception e)
            {
                h.WriteToLog("ParseORU error - " + e.Message);
                return null;
            }
        }

        private static ReturnDataList InterpretData(List<LookupData> dataList)
        {
            ReturnDataList returnList = new ReturnDataList();
            foreach (var data in dataList)
            {
                ReturnData returnData = new ReturnData();
                returnData.OrderID = data.OrderID;
                returnData.OrderName = data.LabName;
                returnData.ResultName = data.ResultName;
                // call the database table 
                using (var db = new InterpretationDBContext())
                {
                    var query = from l in db.LabLookups
                                where l.FacilityName == data.FacilityName && 
                                    l.LabCode == data.LabCode && 
                                    l.ResultCode == data.ResultCode && 
                                    l.ResultValue == data.ResultValue
                                select l;
                    if (query.Count() == 1)
                    {
                        if (query.First().CanSkip == null || query.First().CanSkip == true) continue;
                        //var item = query.ElementAt(0);
                        if (query.First().IsPositive != null)
                        {
                            returnData.IsPositive = query.First().IsPositive.Value;
                        } else // null result, neither positive or negative
                        {
                            SendToPhysicianConsole(data);
                        }
                        returnData.CommonOrderName = query.First().CommonLabName;
                        returnData.CommonResultName = query.First().CommonResultName;
                        
                        returnList.DataList.Add(returnData);
                        //foreach (var item in query)
                        //{
                        //    returnData.IsPositive = item.Value;
                        //    Console.WriteLine(item.ToString());
                        //}


                    } else // either 0 or many rows found
                    {
                        SendToPhysicianConsole(data);
                    }
                }
            }
            return returnList;
        }

        private static void SendToPhysicianConsole(LookupData data)
        {
            throw new NotImplementedException();
        }

        static private void StartWebService()
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
                //BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxReceivedMessageSize = int.MaxValue;
                //binding.MessageEncoding = WSMessageEncoding.Text;
                //binding.TextEncoding = System.Text.Encoding.UTF8;

                host.AddServiceEndpoint(typeof(iContract), binding, "");

                ServiceDebugBehavior stp = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                stp.HttpHelpPageEnabled = true;  // probably remove for prod
                stp.HttpsHelpPageEnabled = true;
                stp.IncludeExceptionDetailInFaults = true;

                host.Open();
            }
            catch (Exception e)
            {
                h.WriteToLog("Error in StartWebService" + e.Message);
            }

        }

        static private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // tick

            //GetDataFromCPS();

        }

    }
}
