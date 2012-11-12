// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

namespace PandoraJs.Utils
{
	public class FileLoader
	{
		public string JsPath;
		public string CssPath;

		//List<string> _loadedCss;
		List<string> _loadedJs;
		RealDictionary _cssObjects;

		private static FileLoader _context;
		public static FileLoader Context
		{
			get
			{
				if (_context == null)
				{
					_context = new FileLoader();
				}
				return _context;
			}
		}

		public FileLoader()
		{
			_cssObjects =  new RealDictionary();
			_loadedJs = new List<string>();
			
		}
		public void ProcessJavascript(string filename, EventHandler e)
		{
			if (!_loadedJs.Contains(filename.ToLowerCase()))
			{
				jQuery.GetScript(JsPath + filename + ".js", new AjaxCallback<object>(delegate(object o) { e.Invoke(this, null); }));
				_loadedJs.Add(filename.ToLowerCase());
			}
			else
			{
				e.Invoke(this, null);
			}
		}

		public void ProcessCSS(string filename, EventHandler e)
		{
			if (!_cssObjects.ContainsKey(filename))
			{
				string filePath = CssPath + filename + ".css?" + DateTime.Now.GetMilliseconds().ToString();

				//jQuery.Get(filePath, delegate(object css)
				//{
				//    string current = jQuery.Select("style").GetHtml();
				//    jQuery.Select("style").Html(current + (string)css);
				//    e.Invoke(this, null);
				//});

				Element fileref = Document.CreateElement("link");
				fileref.SetAttribute("rel", "stylesheet");
				fileref.SetAttribute("type", "text/css");
				fileref.SetAttribute("href", CssPath + filename + ".css?" + DateTime.Now.GetMilliseconds().ToString());
				Document.GetElementsByTagName("head")[0].AppendChild(fileref);
				_cssObjects.Add(filename, fileref);
				//_loadedCss.Add(filename);
				if (e != null)
				{
					e.Invoke(this, null);
				}
				
			}
			else
			{
				Logging.Debug("Already Processed CSS File", new object[] { filename });
				if (e != null)
				{
					e.Invoke(this, null);
				}
			} 
			
		}

		public void RemoveCSS(string filename)
		{
			if (_cssObjects.ContainsKey(filename))
			{


				//jQuery.Select("link").Each(delegate(int count, Element element)
				//{
				//    string[] arr = jQuery.This.GetAttribute("href").Split('?');
				//    if (arr.Length == 2)
				//    {
				//        if(arr[0] == filename)
				//            jQuery.This.Remove();
				//    }
				//});

				Element e = (Element)_cssObjects[filename];
				if (e != null)
				{
					if (!Script.IsNullOrUndefined(e.ParentNode))
					{
						e.ParentNode.RemoveChild(e);
					}
					else
					{
						Logging.Warn("Link Parent Node not found, styling issues may occur as we are unable to delete the link tag", null);
					}
				}
				_cssObjects.Remove(filename);
				
			}
		}

	}
}
