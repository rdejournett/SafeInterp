using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using NHapi.Base.Parser;
using System.Linq;
using Newtonsoft.Json;

namespace InterpCheckSvc
{
    class WebService : iContract
    {

        Helper h = new InterpCheckSvc.Helper();
        //public CRUD crud = new CRUD();
        public Config config = new Config();

        public bool isTest = false;

        public WebService(Config config)
        {
            this.config = config;
        }
        public WebService() { }

        public string TestMethod()
        {
            h.WriteToLog("In TestMethod");
            return "SUCCESS";
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
                h.WriteToLog("StreamToString error - " + e.Message);
                return "";

            }

        }

        public Stream InterpretORU(Stream ORUMessageStream)
        {
            try
            {

                string ORUMessageString = StreamToString(ORUMessageStream);

                // get list of lab results
                List<LookupData> dataList = ParseORU(ORUMessageString);
                foreach (var data in dataList)
                {
                    h.WriteToLog("Parsed Lab: " + data.LabName + " Result: " + data.ResultName);
                }
                // interpret the list
                ReturnDataList returnDataList = InterpretData(dataList);
                h.WriteToLog("Past Interpret Data");
                // send the data back
                foreach (var data in returnDataList.DataList)
                {
                    h.WriteToLog("ReturnDataList Order Name:" + data.CommonOrderName + " Result: " + data.CommonResultName + " Positive:" + data.IsPositive.ToString());
                }
                string json = JsonConvert.SerializeObject(returnDataList, Newtonsoft.Json.Formatting.Indented);
                h.WriteToLog("past Json Convert");
                return StringToStream(json);
            }
            catch (Exception e)
            {
                h.WriteToLog("InterpretORU error - " + e.Message);
                return StringToStream("InterpretORU error - " + e.Message);
            }
        }

        public Stream InterpretORUTest(Stream ORUMessageStream)
        {
            h.WriteToLog("In InterpretORUTest");
            string hl7 = StreamToString(ORUMessageStream);

            return StringToStream(hl7);

        }


        private List<LookupData> ParseORU(string ORUMessageString)
        {
            PipeParser pipeParser = new PipeParser();
            try
            {
                NHapi.Base.Model.IMessage message = pipeParser.Parse(ORUMessageString);
                List<LookupData> dataList = new List<LookupData>();
                h.WriteToLog("In ParseORU");
                if (message is NHapi.Model.V251.Message.ORU_R01)
                {
                    NHapi.Model.V251.Message.ORU_R01 ORU = (NHapi.Model.V251.Message.ORU_R01)message;
                    h.WriteToLog("In ParseORU2 " + ORU.MSH.SendingApplication.NamespaceID.Value);
                    h.WriteToLog("In ParseORU2 " + ORU.PATIENT_RESULTRepetitionsUsed.ToString());
                    foreach (var ptresults in ORU.PATIENT_RESULTs)
                    {
                        h.WriteToLog("In ParseORU3");
                        foreach (var order in ptresults.ORDER_OBSERVATIONs)
                        {
                            h.WriteToLog("In ParseORU4");
                            h.WriteToLog("Order: " + order.OBR.FillerOrderNumber.EntityIdentifier.Value);
                            //string orderID = order.OBR.FillerOrderNumber.EntityIdentifier.Value;
                            //string orderName = order.OBR.UniversalServiceIdentifier.Identifier.Value;
                            // loop on OBX
                            foreach (var result in order.OBSERVATIONs)
                            {
                                h.WriteToLog("In ParseORU5");
                                h.WriteToLog("Result: " + result.OBX.ObservationIdentifier.Text.Value);
                                LookupData data = new InterpCheckSvc.LookupData();
                                // get lab facility
                                if (ORU.MSH.SendingApplication.NamespaceID.Value == "QLS") data.FacilityName = "Quest";
                                data.OrderID = order.OBR.FillerOrderNumber.EntityIdentifier.Value;
                                data.LabCode = order.OBR.UniversalServiceIdentifier.Identifier.Value; // OBR4.1
                                data.LabName = order.OBR.UniversalServiceIdentifier.Text.Value; // OBR4.2
                                data.ResultCode = result.OBX.ObservationIdentifier.Identifier.Value; // OBX3.1
                                data.ResultName = result.OBX.ObservationIdentifier.Text.Value; // OBX3.2
                                NHapi.Base.Model.IType obsData = result.OBX.GetObservationValue(0).Data; // OBX5
                                if (obsData.TypeName == "ST")
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


        private ReturnDataList InterpretData(List<LookupData> dataList)
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
                        if (query.First().IsPositive != null)
                        {
                            returnData.IsPositive = query.First().IsPositive.Value;
                        }
                        else // null result, neither positive or negative
                        {
                            SendToPhysicianConsole(data);
                        }
                        returnData.CommonOrderName = query.First().CommonLabName;
                        returnData.CommonResultName = query.First().CommonResultName;
                        returnList.DataList.Add(returnData);

                    }
                    else // either 0 or many rows found
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


    }
}
