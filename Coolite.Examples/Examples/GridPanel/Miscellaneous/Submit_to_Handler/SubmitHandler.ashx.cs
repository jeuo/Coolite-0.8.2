using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Xml;
using Coolite.Ext.Web;

namespace Coolite.Examples.Examples.GridPanel.Miscellaneous.Submit_to_Handler
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SubmitHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Coolite.Ext.Web.SubmitHandler submitData = new Coolite.Ext.Web.SubmitHandler(context);
            Response response = new Response(true);

            try
            {
                string json = submitData.Json;
                XmlNode xml = submitData.Xml;
                List<Country> objects = submitData.Object<Country>();
                //process data
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Msg = e.Message;
            }

            response.Write();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class Country
    {
        public string Name { get; set; }
    }
}
