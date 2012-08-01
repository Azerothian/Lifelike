using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;

namespace Illisian.Lifelike._admin.controls
{
 
    public partial class ctlSiteManager : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                storeSites.DataSource = new Site[] {
                new Site() { Id = Guid.NewGuid(), HostName = "www.illisian.com.au", SiteName = "illisian.com.au", StartItem = "/lifelife/content/illisian.com.au", DefaultLanguage = "jp_JP" },
                new Site() { Id = Guid.NewGuid(), HostName = "www.augaming.org", SiteName = "augaming.org", StartItem = "/lifelife/content/augaming.org", DefaultLanguage = "en_US" },
                new Site() { Id = Guid.NewGuid(), HostName = "www.nadir.org", SiteName = "nadir.org", StartItem = "/lifelife/content/nadir.org", DefaultLanguage = "en_US" }
            };
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

        class Site
        {
            public Guid Id { get; set; }
            public string SiteName { get; set; }
            public string HostName { get; set; }
            public string StartItem { get; set; }

        }
    }
}