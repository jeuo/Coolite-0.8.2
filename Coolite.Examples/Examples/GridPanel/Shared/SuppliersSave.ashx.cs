using System;
using System.Data.Linq;
using System.Web;
using Coolite.Examples.Code.Northwind;
using System.Web.Services;
using Coolite.Ext.Web;

namespace Coolite.Examples.Examples.GridPanel.Shared
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SuppliersSave : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Response sr = new Response(true);

            try
            {
                NorthwindDataContext db = new NorthwindDataContext();
                StoreDataHandler dataHandler = new StoreDataHandler(context);
                ChangeRecords<Supplier> data = dataHandler.ObjectData<Supplier>();

                foreach (Supplier supplier in data.Deleted)
                {
                    db.Suppliers.Attach(supplier);
                    db.Suppliers.DeleteOnSubmit(supplier);
                }
                
                foreach (Supplier supplier in data.Updated)
                {
                    db.Suppliers.Attach(supplier);
                    db.Refresh(RefreshMode.KeepCurrentValues, supplier);
                }

                foreach (Supplier supplier in data.Created)
                {
                    db.Suppliers.InsertOnSubmit(supplier);
                }

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                sr.Success = false;
                sr.Msg = e.Message;
            }

            sr.Write();
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
