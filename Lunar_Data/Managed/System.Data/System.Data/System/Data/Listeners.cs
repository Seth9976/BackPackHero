using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000DB RID: 219
	internal sealed class Listeners<TElem> where TElem : class
	{
		// Token: 0x06000C80 RID: 3200 RVA: 0x000395C1 File Offset: 0x000377C1
		internal Listeners(int ObjectID, Listeners<TElem>.Func<TElem, bool> notifyFilter)
		{
			this._listeners = new List<TElem>();
			this._filter = notifyFilter;
			this._objectID = ObjectID;
			this._listenerReaderCount = 0;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x000395E9 File Offset: 0x000377E9
		internal bool HasListeners
		{
			get
			{
				return 0 < this._listeners.Count;
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x000395F9 File Offset: 0x000377F9
		internal void Add(TElem listener)
		{
			this._listeners.Add(listener);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00039607 File Offset: 0x00037807
		internal int IndexOfReference(TElem listener)
		{
			return Index.IndexOfReference<TElem>(this._listeners, listener);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00039618 File Offset: 0x00037818
		internal void Remove(TElem listener)
		{
			int num = this.IndexOfReference(listener);
			this._listeners[num] = default(TElem);
			if (this._listenerReaderCount == 0)
			{
				this._listeners.RemoveAt(num);
				this._listeners.TrimExcess();
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00039664 File Offset: 0x00037864
		internal void Notify<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, Listeners<TElem>.Action<TElem, T1, T2, T3> action)
		{
			int count = this._listeners.Count;
			if (0 < count)
			{
				int num = -1;
				this._listenerReaderCount++;
				try
				{
					for (int i = 0; i < count; i++)
					{
						TElem telem = this._listeners[i];
						if (this._filter(telem))
						{
							action(telem, arg1, arg2, arg3);
						}
						else
						{
							this._listeners[i] = default(TElem);
							num = i;
						}
					}
				}
				finally
				{
					this._listenerReaderCount--;
				}
				if (this._listenerReaderCount == 0)
				{
					this.RemoveNullListeners(num);
				}
			}
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00039710 File Offset: 0x00037910
		private void RemoveNullListeners(int nullIndex)
		{
			int num = nullIndex;
			while (0 <= num)
			{
				if (this._listeners[num] == null)
				{
					this._listeners.RemoveAt(num);
				}
				num--;
			}
		}

		// Token: 0x040007FF RID: 2047
		private readonly List<TElem> _listeners;

		// Token: 0x04000800 RID: 2048
		private readonly Listeners<TElem>.Func<TElem, bool> _filter;

		// Token: 0x04000801 RID: 2049
		private readonly int _objectID;

		// Token: 0x04000802 RID: 2050
		private int _listenerReaderCount;

		// Token: 0x020000DC RID: 220
		// (Invoke) Token: 0x06000C88 RID: 3208
		internal delegate void Action<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

		// Token: 0x020000DD RID: 221
		// (Invoke) Token: 0x06000C8C RID: 3212
		internal delegate TResult Func<T1, TResult>(T1 arg1);
	}
}
