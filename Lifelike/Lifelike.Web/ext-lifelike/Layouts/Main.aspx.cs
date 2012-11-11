using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Lifelike.Kernel.Entities;
using Lifelike.Kernel.EntityLogic;
using Lifelike.Kernel.WebComponents;

namespace Lifelike.WebAdmin.lifelike.Layouts
{
	public partial class Main : Lifelike.Kernel.WebComponents.Layout
	{
		//<ext:DesktopModule ModuleID="dmItemEditor">
		//	<Window>
		//		<ext:Window ID="winItemEditor" runat="server" Width="600" Height="400" Title="Item Editor"
		//			Maximizable="true" Minimizable="true" Layout="BorderLayout">
		//			<Content>
		//				<%--<ll:ItemEditor ID="llItemEditor" runat="server" />--%>
		//			</Content>
		//		</ext:Window>
		//	</Window>
		//	<Shortcut Name="Item Editor" SortIndex="3" />
		//	<Launcher Text="Item Editor" />
		//</ext:DesktopModule>

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!X.IsAjaxRequest)
			{
				var path = "/system/admin/windows/itemeditor";
				var session = Lifelike.Kernel.Database.Context.OpenSession();
				using (var tx = session.BeginTransaction())
				{
					Item editor = ItemLogic.GetItemFromPath(session, tx, path, true);
					if (string.IsNullOrEmpty(editor.Value))
					{

						editor.Value = TemplateLogic.CreateTemplateFromView(ViewLogic.GetCurrentView(editor));
						editor.Save(session, tx);
						tx.Commit();

					}
				}
				path = "/system/admin/windows";

				Item item = ItemLogic.GetItemFromPath(path, true);


				List<Lifelike.Kernel.Entities.Module> m = new List<Lifelike.Kernel.Entities.Module>();
				if (item.Children != null)
				{
					foreach (var c in item.Children)
					{
						//	var view = ViewLogic.GetCurrentView(c);
						//var td = TemplateLogic.LoadFromItem(c);
						createDesktopModule(c);

					}
				}
			}
		}
		private void createDesktopModule(Item c)
		{
			//DesktopModule dm = new DesktopModule(new DesktopModule.Config()
			//		{
			//			ModuleID = "dm" + c.Name
			//		});
			//dm.Shortcut = new DesktopShortcut(new DesktopShortcut.Config() { Name = c.Name });
			//dm.Launcher = new Ext.Net.MenuItem(new Ext.Net.MenuItem.Config() { Text = c.Name });
			//dm.Window.Add(new ModuleWindow(ModuleLogic.GetAllWebCtlModulesFromItemByCurrentView(this, c)));
			//Desktop.GetInstance().AddModule(dm);

			//Title="Item Editor"
			//			Maximizable="true" Minimizable="true" Layout="BorderLayout
			var window = new Window
			{
				Title = c.Name,
				Width = 600,
				Height = 600,
				DefaultRenderTo = Ext.Net.DefaultRenderTo.Form,
				Icon = Icon.ApplicationAdd,
				Maximizable = true,
				Minimizable = true,
				Layout = "BorderLayout"
			};
			foreach (var m in ViewLogic.GetCurrentView(c).Modules)
			{

				var control = Ext.Net.Utilities.ControlUtils.FindControl<Ext.Net.DesktopModuleProxy>(this.LoadControl("~" + m.Module.Path));
				//control. = this.resManager;
				control.RegisterModule();
				window.Controls.Add(control);
			}
			//foreach (var vcontrol in ModuleLogic.GetAllWebCtlModulesFromItemByCurrentView(this, c))
			//{

			//}

			//var mod = new DesktopModule
			//{
			//	ModuleID = "dm" + c.Name,
			//	Shortcut = new DesktopShortcut
			//	{
			//		Name = c.Name
			//	},

			//	Launcher = new Ext.Net.MenuItem
			//	{
			//		Text = c.Name
			//	},

			//	Window = { window },

			//	AutoRun = true
			//};

			//dtMain.Modules.Add(mod);

		}
		//class ModuleWindow : AbstractWindow
		//{
		//	public ModuleWindow(Lifelike.Kernel.WebComponents.Module[] controls)
		//	{
		//		foreach (var cc in controls)
		//		{
		//			this.Controls.Add(cc);
		//		}
		//	}
		//}

	}
}