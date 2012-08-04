using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Illisian.Lifelike.Data;

namespace Illisian.Lifelike._admin.controls
{

    public partial class ctlSiteManager : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                storeSites.DataBind();
            }



        }

        [DirectMethod]
        public void EditRow(Guid id, string field, string oldValue, string newValue, object customer)
        {
            string message = "<b>Property:</b> {0}<br /><b>Field:</b> {1}<br /><b>Old Value:</b> {2}<br /><b>New Value:</b> {3}";

            // Send Message...
            X.Msg.Notify("Edit Record #" + id.ToString(), string.Format(message, id, field, oldValue, newValue)).Show();

            this.gpSites.GetStore().GetById(id).Commit();
        }

    }
}