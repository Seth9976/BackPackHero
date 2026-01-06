using System;
using System.Collections.Generic;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000193 RID: 403
	public sealed class fsContext
	{
		// Token: 0x06000A9C RID: 2716 RVA: 0x0002CE38 File Offset: 0x0002B038
		public void Reset()
		{
			this._contextObjects.Clear();
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002CE45 File Offset: 0x0002B045
		public void Set<T>(T obj)
		{
			this._contextObjects[typeof(T)] = obj;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002CE62 File Offset: 0x0002B062
		public bool Has<T>()
		{
			return this._contextObjects.ContainsKey(typeof(T));
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002CE7C File Offset: 0x0002B07C
		public T Get<T>()
		{
			object obj;
			if (this._contextObjects.TryGetValue(typeof(T), out obj))
			{
				return (T)((object)obj);
			}
			string text = "There is no context object of type ";
			Type typeFromHandle = typeof(T);
			throw new InvalidOperationException(text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null));
		}

		// Token: 0x04000279 RID: 633
		private readonly Dictionary<Type, object> _contextObjects = new Dictionary<Type, object>();
	}
}
