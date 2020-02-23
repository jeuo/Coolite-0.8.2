using System.ComponentModel;
using System.Web.Services;

namespace Coolite.Examples.Examples.Events.AjaxMethods.WebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class TestXmlService : System.Web.Services.WebService
    {
        [WebMethod]
        public string SayHello(string name)
        {
            return "Hello, " + name;
        }
    }
}