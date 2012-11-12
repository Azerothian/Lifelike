// JQueryEventItem.cs
//

using System;
using System.Collections.Generic;
using jQueryApi;
using System.Collections;

using PandoraJs.Utils.Extension;
using PandoraJs.Utils;

namespace PandoraJs.Events
{
	public class PandoraEventManager
	{
		private Control _source;
		private string _eventName;		
		private List<PandoraEventItem> _eventList = new List<PandoraEventItem>();
		public PandoraEventManager(Control source,string eventName)
		{
			_eventName = eventName;
			_source = source;
		}

		public void Add(PandoraEventHandler eventHandler, Dictionary eventData)
		{
			PandoraEventItem newEventItem = new PandoraEventItem(_source, eventHandler, eventData);
			Bind(newEventItem);
			_eventList.Add(newEventItem);
			
		}

		public void Bind(PandoraEventItem eventItem)
		{
			_source.SetBind(_eventName, new Dictionary<string, object>("eventItem", eventItem), EventHook);
		}
		public void Clear()
		{
			_source.Unbind(_eventName);
			_eventList.Clear();
		}
		public void Fire()
		{
			foreach (PandoraEventItem item in _eventList)
			{
				item.EventHandler.Invoke(null);
			}
		}

		public void Rebind()
		{
			_source.Unbind(_eventName);
			foreach (PandoraEventItem item in _eventList)
			{
				Bind(item);
			}
		}


		private void EventHook(object source, object[] args)
		{
			jQueryEvent e = (jQueryEvent)source;
			PandoraEventItem eventItem = (PandoraEventItem) e.Data["eventItem"];
			List<object> objArr = new List<object>();
			
			
			objArr.Add(e);

			if (args != null)
			{
				foreach (object o in args)
				{
					objArr.Add(o);
				}
			}

			eventItem.Invoke(objArr);
		}


	}


	
}
