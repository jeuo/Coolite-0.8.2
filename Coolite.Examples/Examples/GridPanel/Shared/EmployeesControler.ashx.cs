using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Services;
using Coolite.Examples.Code.Northwind;
using Coolite.Ext.Web;

namespace Coolite.Examples.Examples.GridPanel.Shared
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class EmployeesControler : IHttpHandler
    {
        private HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            context.Response.ContentType = "text/json";

            int count;
            var result = Employee.GetEmployeesFilter(this.Start, this.Limit, this.Sort, this.Dir, out count);
            var pagingEmployees = new Paging<Employee>(result, count);
            //context.Response.Write(string.Format("{{totalCount:{1},'Employees':{0}}}", JSON.Serialize(pagingEmployees.Data), pagingEmployees.TotalRecords));
            StoreResponseData sr = new StoreResponseData();
            sr.TotalCount = pagingEmployees.TotalRecords;
            sr.Data = JSON.Serialize(pagingEmployees.Data);
            sr.Return();
        }

        #region Variables from request

        int Start
        {
            get
            {
                string startStr = context.Request["start"];
                if (string.IsNullOrEmpty(startStr))
                {
                    throw new NullReferenceException("Start is absent");
                }

                return int.Parse(startStr);
            }
        }

        int Limit
        {
            get
            {
                string limitStr = context.Request["limit"];
                if (string.IsNullOrEmpty(limitStr))
                {
                    throw new NullReferenceException("Limit is absent");
                }

                return int.Parse(limitStr);
            }
        }

        string Sort
        {
            get
            {
                string sort = context.Request["sort"];
                return sort;
            }
        }

        string Dir
        {
            get
            {
                string dir = context.Request["dir"];
                return dir;
            }
        }

        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
