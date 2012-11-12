// Request.cs
//

using System;
using System.Collections.Generic;
using jQueryApi;
using PandoraJs.Utils.Extension;
using System.Collections;

namespace PandoraJs.Utils
{
	public class Request
	{
		



		public static void Get(string url, AjaxCallback<object> target)
		{
			jQuery.Get(AppendUrlTimeStamp(url), target);

		}
		public static void PostComplexToMVC(string url, Dictionary<string, string> formData, AjaxRequestCallback<object> ajaxRequestCallback)
		{
			
			//jQueryAjaxOptions ajaxOptions = new jQueryAjaxOptions();
			//ajaxOptions.Url = AppendUrlTimeStamp(url);
			//ajaxOptions.Type = "POST";
			//ajaxOptions.DataType = "json";
			//ajaxOptions.ContentType = "application/json; charset=utf-8";
			//ajaxOptions.Data = Json.toJSON(data);
			//ajaxOptions.Success = ajaxRequestCallback;
			//jQuery.Ajax(ajaxOptions);
			//Postify mvcPostify = new Postify();
			//object data = mvcPostify.CreatePropertyObject(formData);
			//object postData = mvcPostify.Parse(data, 10);

			//Logging.Debug("Posting Complex Data Object for Asp.net MVC", new object[] { postData });
			//jQuery.Post(AppendUrlTimeStamp(url), postData, ajaxRequestCallback);

		}
		public static void Post(string url, object formData, AjaxRequestCallback<object> ajaxRequestCallback)
		{

			jQuery.Post(AppendUrlTimeStamp(url), formData, ajaxRequestCallback);
		}
		private static string AppendUrlTimeStamp(string url)
		{
			if (url.IndexOf('?') > 0)
			{
				url = url + "&";
			}
			else
			{
				url = url + "?";
			}
			return url + "requestTimeStamp=" + DateTime.Now.ToTimeString();
		}
	}
}
