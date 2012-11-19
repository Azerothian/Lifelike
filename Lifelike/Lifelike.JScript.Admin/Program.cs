using System;
using System.Collections.Generic;
using System.Html;
using System.Linq;
using System.Text;
using Lifelike.JScript.Admin.jQueryUI;
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
				//Tree tree = new Tree(".itemEditor");

				//var node = new Node() { Text = "Text", Value = "Value" };
				//node.Children = new List<Node>()
				// {
				//	 new Node() { Text = "Text", Value = "Value" , Parent = node },
				//	 new Node() { Text = "Text", Value = "Value", Parent = node },
				//	 new Node() { Text = "Text", Value = "Value", Parent = node },
					 
				// };
				//var ss = new Node() { Text = "Text", Value = "Value", Parent = node };
				//ss.Children = new List<Node>()
				// {
				//	 new Node() { Text = "Text", Value = "Value" , Parent = node },
				//	 new Node() { Text = "Text", Value = "Value", Parent = node },
				//	 new Node() { Text = "Text", Value = "Value", Parent = node },
					 
				// };
				//node.Children.Add(ss);
				//tree.AddNode(null, node);

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
				PageManager.Context.Initialise();
				PageRenderer.Context.Render();
				HubManager.Context.Initialise();
				HubManager.Context.OnConnection += Context_OnConnection;
			});


		}

		static void Context_OnConnection(bool msg1)
		{
			
		}
	}
}
