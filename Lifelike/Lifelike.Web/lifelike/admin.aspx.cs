using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Lifelike.Kernel.Entities;
using Lifelike.Kernel.EntityLogic;

namespace Lifelike.WebAdmin.lifelike
{
	public partial class admin : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			string path = "/system/admin/windows";

			Item item = ItemLogic.GetItemFromPath(path, true);

			path = "/system/admin/windows/itemeditor";
			Item editor = ItemLogic.GetItemFromPath(path, true);

			Lifelike.Kernel.Entities.View view = new Lifelike.Kernel.Entities.View();
			


			List<Module> m = new List<Module>();
			if (item.Children != null)
			{
				foreach (var c in item.Children)
				{
					var td = TemplateLogic.LoadFromItem(c);
					DesktopModule dm = new DesktopModule(new DesktopModule.Config()
					{
						ModuleID = "dm" + td["Name"].Value
					});
					dm.Window.Add(new ModuleWindow(ModuleLogic.GetAllWebCtlModulesFromItemByCurrentView(this, c)));
				}
			}
			//if(

			//item.Value = TemplateLogic.LoadFromItem();



			
			//dm.Window.Add(new ItemWindow());
			//dm

			//dtMain.Modules.
		}
		class ModuleWindow : AbstractWindow
		{
			public ModuleWindow(Lifelike.Kernel.WebComponents.Module[] controls)
			{
				foreach (var cc in controls)
				{
					this.Controls.Add(cc);
				}
			}
		}
	}
}