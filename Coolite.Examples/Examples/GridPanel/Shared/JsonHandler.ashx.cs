using System.Web;
using System.Web.Services;
using Coolite.Ext.Web;

namespace Coolite.Examples.Examples.GridPanel.Shared
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class JsonHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            context.Response.Write(string.Format("{{'plants':{0}}}", JSON.Serialize(Plant.TestData)));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
