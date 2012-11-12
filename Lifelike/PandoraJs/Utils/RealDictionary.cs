// RealDictionary.cs
//

using System;
using System.Collections.Generic;


namespace PandoraJs.Utils
{

	public delegate void RealDictionaryAddEventHandler(object key, object item);
	public delegate void RealDictionaryRemoveEventHandler(object key);
	public class RealDictionary
	{
		public RealDictionaryAddEventHandler OnAdd = null;
		public RealDictionaryRemoveEventHandler OnRemove = null;

		List<RealKeyValuePair> _data = new List<RealKeyValuePair>();
		public void Add(object key, object value)
		{
			if (!ContainsKey(key))
			{
				RealKeyValuePair _newKeyPair = new RealKeyValuePair();
				_newKeyPair.Key = key;
				_newKeyPair.Value = value;
				_data.Add(_newKeyPair);
				if (OnAdd != null)
					OnAdd(key, value);
			}
		}
		public void Remove(object key)
		{
			int removeAt = -1;
			for (int i = 0; i < _data.Count; i++)
			{
				Logging.Debug("Comparing Keys", new object[] { _data[i].Key, _data[i].Key == key, key});
				if (_data[i].Key == key)
				{
					removeAt = i;
					break;
				}
			}

			if (removeAt > -1)
			{
				if (OnRemove != null)
					OnRemove.Invoke(key);
				_data.RemoveAt(removeAt);
			}
			else
			{
				Logging.Warn("Real Dictionary does not contain the provided key", new object[] { this, key });
			}
		}

		public object this[object key]
		{
			get
			{
				return Get(key);
			}
			set
			{
				if (!ContainsKey(key))
				{
					Add(key, value);
				}
				else
				{
					Set(key, value);
				}

			}
		}

		private void Set(object key, object value)
		{
			for (int i = 0; i < _data.Count - 1; i++)
			{
				if (_data[i].Key == key)
				{
					_data[i].Value = value;
					break;
				}
			}
		}

		public object Get(object key)
		{
			foreach (RealKeyValuePair keypair in _data)
			{
				if (keypair.Key == key)
					return keypair.Value;
			}
			return null;
		}
		//public T Getz<T>(object key)
		//{
		//    return (T)Get(key);
		//}

		public bool ContainsKey(object key)
		{
			foreach (RealKeyValuePair keypair in _data)
			{
				if (keypair.Key == key)
				{
					return true;
				}
			}
			return false;
		}

		public IEnumerable<object> GetAllValues()
		{
			List<object> _returnval = new List<object>();
			foreach (RealKeyValuePair keypair in _data)
			{
				_returnval.Add(keypair.Value);
			}
			return _returnval;
		}
	}
	public class RealKeyValuePair
	{

		public RealKeyValuePair() { }
		public object Key;
		public object Value;
	}

}
