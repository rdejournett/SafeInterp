using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using System.ServiceModel.Channels;

namespace InterpConsoleApp
{
    [ServiceContract]
    interface iContract
    {
        [OperationContract]
        [WebGet]
        string TestMethod();

        //[WebInvoke(Method = "POST",
        //    UriTemplate = "InterpSvc/{patientid}",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped)]
        //Stream PatientCheckin(string apptid);

        //// FOR THE LOVE OF ALL THAT IS HOLY, set Content-Type to application/json
        //[WebInvoke(Method = "POST",
        //    UriTemplate = "Checkin/PatientDemos/{PatientProfileId}",
        //    //UriTemplate = "Checkin/PatientDemos",
        //    BodyStyle = WebMessageBodyStyle.Bare, // this is key for combining params with Stream
        //    ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //Stream PatientDemos(string PatientProfileId, ApptData data);


        //[WebInvoke(Method = "GET",
        //    UriTemplate = "InterpSvc?patientid={patientid}",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped)]
        //Stream GetData(string patientid);

        [WebInvoke(Method = "POST",
            UriTemplate = "InterpHL7",
            BodyStyle = WebMessageBodyStyle.Bare)] // this is key for combining params with Stream
        Stream InterpretORU(Stream ORU);

        [WebInvoke(Method = "POST",
            UriTemplate = "InterpSvcTest",
            BodyStyle = WebMessageBodyStyle.Bare)] // this is key for combining params with Stream

        Stream InterpretORUTest(Stream ORU);

        //[WebInvoke(Method = "GET",
        //    UriTemplate = "Checkin/GetDataEncrypt?start={start}&end={end}&since={since}",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped)]
        //Stream GetDataEncrypt(string start, string end, string since);

        //[WebInvoke(Method = "GET",
        //    UriTemplate = "Checkin/GetConfig",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped)]
        //Stream GetConfig();

        //// this is for development - creates an appts

        //[WebInvoke(Method = "POST",
        //    UriTemplate = "Checkin/AddAppointment",
        //    BodyStyle = WebMessageBodyStyle.Bare, // this is key for combining params with Stream
        //    ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //Stream AddAppointment(ApptData data);

        //[WebInvoke(Method = "POST",
        //    UriTemplate = "Checkin/DeleteAppointment",
        //    BodyStyle = WebMessageBodyStyle.Bare, // this is key for combining params with Stream
        //    ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //Stream DeleteAppointment(ApptData data);

        //[WebInvoke(Method = "POST",
        //    UriTemplate = "Checkin/UndoPatientCheckin/{apptid}",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped)]
        //Stream UndoPatientCheckin(string apptid);

        //// pictures

        //[WebInvoke(Method = "GET",
        //    UriTemplate = "Checkin/GetPicture/{PatientProfileId}")]
        //Stream GetPicture(string PatientProfileId);

        //[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Checkin/SetPicture/{PatientProfileId}")]
        //Stream SetPicture(string PatientProfileId, Stream image);

        //[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Checkin/SetDocument/{PatientProfileId}/DocumentName/{Name}")]
        //Stream SetDocument(string PatientProfileId, string Name, Stream image);

    }

    public class RawContentTypeMapper : WebContentTypeMapper
    {
        public override WebContentFormat GetMessageFormatForContentType(string contentType)
        {
            switch (contentType.ToLowerInvariant())
            {
                case "text/plain":
                case "application/json":
                    return WebContentFormat.Json;
                case "application/xml":
                    return WebContentFormat.Xml;
                default:
                    return WebContentFormat.Default;
            }
        }
    }

}
