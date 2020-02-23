using System.Web.Services;
using Coolite.Ext.Web;

namespace Coolite.Examples.Examples.AjaxEvents.Basic.WebService
{
    /// <summary>
    /// Summary description for TestService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TestService : System.Web.Services.WebService
    {
        [WebMethod]
        public AjaxResponse SayHello1(string name)
        {
            AjaxResponse response = new AjaxResponse();
            
            // Return a script to be executed on the client
            response.Script = string.Concat("alert('Hello, ", name, "');");

            return response;
        }

        [WebMethod]
        public AjaxResponse SayHello2(string name)
        {
            AjaxResponse response = new AjaxResponse();
            
            ParameterCollection parameters = new ParameterCollection();

            parameters["Greeting"] = "Hello, " + name;
            
            response.ExtraParamsResponse = parameters.ToJson();

            return response;
        }
    }
}