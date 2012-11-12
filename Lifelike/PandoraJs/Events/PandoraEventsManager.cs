using System;
using System.Collections.Generic;
using jQueryApi;
using System.Collections;

using PandoraJs.Utils;
using PandoraJs.Utils.Extension;

namespace PandoraJs.Events
{

	public class PandoraEventsManager
	{
		RealDictionary _dictionary = null;
		Control _source = null;
		public PandoraEventsManager(Control source)
		{
			_dictionary = new RealDictionary();
			_source = source;
		}


		public PandoraEventManager this[string key]
		{
			get
			{
				if (!_dictionary.ContainsKey(key))
				{
					_dictionary[key] = new PandoraEventManager(_source, key);
				}
				return (PandoraEventManager)_dictionary.Get(key);
			}
			
		}
		public void Rebind()
		{
			foreach (object j in _dictionary.GetAllValues())
			{
				PandoraEventManager manager = (PandoraEventManager)j;
				manager.Rebind();
			}
		}

		public void Clear()
		{
			foreach (object j in _dictionary.GetAllValues())
			{
				PandoraEventManager manager = (PandoraEventManager)j;
				manager.Clear();
			}
		}
	}
		
	
}
