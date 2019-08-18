using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace _00_SimpleWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Idemo" in both code and config file together.
    [ServiceContract]
    public interface Idemo
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string DoWork();
    }
}
