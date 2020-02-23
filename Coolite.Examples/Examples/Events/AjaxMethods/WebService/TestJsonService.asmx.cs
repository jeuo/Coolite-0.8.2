using System.ComponentModel;
using System.Web.Script.Services;
using System.Web.Services;

namespace Coolite.Examples.Examples.Events.AjaxMethods.WebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class TestJsonService : System.Web.Services.WebService
    {
        [WebMethod]
        public string SayHello(string name)
        {
            return "Hello, " + name;
        }
    }
}