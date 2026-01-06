using System;
using System.Collections.Generic;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x0200000E RID: 14
	public abstract class KeyValueDataBase<V> : IKeyValueData<V>
	{
		// Token: 0x0600002F RID: 47 RVA: 0x0000240C File Offset: 0x0000060C
		public void Set(string key, V value)
		{
			if (string.IsNullOrEmpty(key))
			{
				return;
			}
			this._data[key] = value;
			List<Action<V>> list;
			if (!this._callbacks.TryGetValue(key, out list))
			{
				return;
			}
			foreach (Action<V> action in list)
			{
				action(value);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002480 File Offset: 0x00000680
		public bool Has(string key)
		{
			return this._data.ContainsKey(key);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000248E File Offset: 0x0000068E
		public V Get(string key, V defaultValue = default(V))
		{
			if (string.IsNullOrEmpty(key) || !this._data.ContainsKey(key))
			{
				return defaultValue;
			}
			return this._data[key];
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000024B4 File Offset: 0x000006B4
		public void Clear()
		{
			this._data.Clear();
		}

		// Token: 0x06000033 RID: 51
		public abstract string Save();

		// Token: 0x06000034 RID: 52
		public abstract void Load(string save);

		// Token: 0x06000035 RID: 53 RVA: 0x000024C4 File Offset: 0x000006C4
		public void AddKeyListener(string key, Action<V> callback)
		{
			List<Action<V>> list;
			if (!this._callbacks.TryGetValue(key, out list))
			{
				list = new List<Action<V>>();
				this._callbacks[key] = list;
			}
			this._callbacks[key].Add(callback);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002506 File Offset: 0x00000706
		public void RemoveKeyListener(string key, Action<V> callback)
		{
			this._callbacks[key].Remove(callback);
		}

		// Token: 0x04000011 RID: 17
		private readonly Dictionary<string, List<Action<V>>> _callbacks = new Dictionary<string, List<Action<V>>>();

		// Token: 0x04000012 RID: 18
		protected Dictionary<string, V> _data = new Dictionary<string, V>();
	}
}
