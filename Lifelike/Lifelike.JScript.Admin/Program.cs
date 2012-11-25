using System;
using System.Collections.Generic;
using System.Html;
using System.Linq;
using System.Text;
using jQueryApi;
using jQueryApi.UI;
using jQueryApi.UI.Widgets;
using Lifelike.JScript.Admin.Managers;
namespace Lifelike.JScript.Admin
{
	class Program
	{
		static void Main()
		{
			jQuery.OnDocumentReady(() =>
			{
				

				//tree.Render();

				//jQuery.Select(".itemEditor").Dialog(new DialogOptions { AutoOpen = true, Width = 400, Title = "ITEM EDITOR" });
				//jQuery.Select(".button").Button();
				//jQuery.Select(".expand").Click(p => {

				//	node.Expand();

				//});
				//jQuery.Select(".close").Click(p =>
				//{

				//	node.Close();

				//});
				Log.log("Starting HubManager");
				HubManager.Context.Initialise();
				Log.log("Starting PageManager");
				PageManager.Context.Initialise();
				Log.log("Starting PageRenderer");
				PageRenderer.Context.Render();

				HubManager.Context.OnConnection += Context_OnConnection;
			});


		}

		static void Context_OnConnection(bool msg1)
		{
			
		}
	}
}
