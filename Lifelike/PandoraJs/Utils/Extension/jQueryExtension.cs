// Plugin1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using System.Runtime.CompilerServices;
using jQueryApi;
namespace PandoraJs.Utils.Extension
{
	public class jQueryExtension 
	{
		[ScriptAlias("$")]
		public static T Select<T>(jQueryObject element) where T : jQueryObject
		{
			return default(T);
		}

		[ScriptAlias("$")]
		public static T Select<T>(string selector) where T : jQueryObject
		{
			return default(T);
		}

		[ScriptAlias("$")]
		public static T FromObject<T>(object o) where T : jQueryObject
		{
			return default(T);
		}



	}
}

