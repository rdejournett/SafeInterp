using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;

namespace InterpCheckSvc
{
    [ServiceContract]
    interface iContract
    {
        [OperationContract]
        [WebGet]
        string TestMethod();

        [WebInvoke(Method = "POST",
            UriTemplate = "InterpretORU",
            BodyStyle = WebMessageBodyStyle.Bare // this is key for combining params with Stream
            )]
        Stream InterpretORU(Stream data);

        [WebInvoke(Method = "POST",
            UriTemplate = "InterpretORUTest",
            BodyStyle = WebMessageBodyStyle.Bare // this is key for combining params with Stream
            )]
        Stream InterpretORUTest(Stream data);




    }
}
