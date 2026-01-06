using System;
using System.Threading;

namespace System.Data.ProviderBase
{
	// Token: 0x02000301 RID: 769
	internal abstract class DbReferenceCollection
	{
		// Token: 0x060022F2 RID: 8946 RVA: 0x000A0B79 File Offset: 0x0009ED79
		protected DbReferenceCollection()
		{
			this._items = new DbReferenceCollection.CollectionEntry[20];
			this._itemLock = new object();
			this._optimisticCount = 0;
			this._lastItemIndex = 0;
		}

		// Token: 0x060022F3 RID: 8947
		public abstract void Add(object value, int tag);

		// Token: 0x060022F4 RID: 8948 RVA: 0x000A0BA8 File Offset: 0x0009EDA8
		protected void AddItem(object value, int tag)
		{
			bool flag = false;
			object itemLock = this._itemLock;
			lock (itemLock)
			{
				for (int i = 0; i <= this._lastItemIndex; i++)
				{
					if (this._items[i].Tag == 0)
					{
						this._items[i].NewTarget(tag, value);
						flag = true;
						break;
					}
				}
				if (!flag && this._lastItemIndex + 1 < this._items.Length)
				{
					this._lastItemIndex++;
					this._items[this._lastItemIndex].NewTarget(tag, value);
					flag = true;
				}
				if (!flag)
				{
					for (int j = 0; j <= this._lastItemIndex; j++)
					{
						if (!this._items[j].HasTarget)
						{
							this._items[j].NewTarget(tag, value);
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					Array.Resize<DbReferenceCollection.CollectionEntry>(ref this._items, this._items.Length * 2);
					this._lastItemIndex++;
					this._items[this._lastItemIndex].NewTarget(tag, value);
				}
				this._optimisticCount++;
			}
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000A0CF8 File Offset: 0x0009EEF8
		internal T FindItem<T>(int tag, Func<T, bool> filterMethod) where T : class
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag && this._optimisticCount > 0)
				{
					for (int i = 0; i <= this._lastItemIndex; i++)
					{
						if (this._items[i].Tag == tag)
						{
							object target = this._items[i].Target;
							if (target != null)
							{
								T t = target as T;
								if (t != null && filterMethod(t))
								{
									return t;
								}
							}
						}
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
			return default(T);
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000A0DA0 File Offset: 0x0009EFA0
		public void Notify(int message)
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag)
				{
					try
					{
						this._isNotifying = true;
						if (this._optimisticCount > 0)
						{
							for (int i = 0; i <= this._lastItemIndex; i++)
							{
								object target = this._items[i].Target;
								if (target != null)
								{
									this.NotifyItem(message, this._items[i].Tag, target);
									this._items[i].RemoveTarget();
								}
							}
							this._optimisticCount = 0;
						}
						if (this._items.Length > 100)
						{
							this._lastItemIndex = 0;
							this._items = new DbReferenceCollection.CollectionEntry[20];
						}
					}
					finally
					{
						this._isNotifying = false;
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
		}

		// Token: 0x060022F7 RID: 8951
		protected abstract void NotifyItem(int message, int tag, object value);

		// Token: 0x060022F8 RID: 8952
		public abstract void Remove(object value);

		// Token: 0x060022F9 RID: 8953 RVA: 0x000A0E78 File Offset: 0x0009F078
		protected void RemoveItem(object value)
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag && this._optimisticCount > 0)
				{
					for (int i = 0; i <= this._lastItemIndex; i++)
					{
						if (value == this._items[i].Target)
						{
							this._items[i].RemoveTarget();
							this._optimisticCount--;
							break;
						}
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000A0EFC File Offset: 0x0009F0FC
		private void TryEnterItemLock(ref bool lockObtained)
		{
			lockObtained = false;
			while (!this._isNotifying && !lockObtained)
			{
				Monitor.TryEnter(this._itemLock, 100, ref lockObtained);
			}
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x000A0F1F File Offset: 0x0009F11F
		private void ExitItemLockIfNeeded(bool lockObtained)
		{
			if (lockObtained)
			{
				Monitor.Exit(this._itemLock);
			}
		}

		// Token: 0x0400175E RID: 5982
		private const int LockPollTime = 100;

		// Token: 0x0400175F RID: 5983
		private const int DefaultCollectionSize = 20;

		// Token: 0x04001760 RID: 5984
		private DbReferenceCollection.CollectionEntry[] _items;

		// Token: 0x04001761 RID: 5985
		private readonly object _itemLock;

		// Token: 0x04001762 RID: 5986
		private int _optimisticCount;

		// Token: 0x04001763 RID: 5987
		private int _lastItemIndex;

		// Token: 0x04001764 RID: 5988
		private volatile bool _isNotifying;

		// Token: 0x02000302 RID: 770
		private struct CollectionEntry
		{
			// Token: 0x060022FC RID: 8956 RVA: 0x000A0F2F File Offset: 0x0009F12F
			public void NewTarget(int tag, object target)
			{
				if (this._weak == null)
				{
					this._weak = new WeakReference(target, false);
				}
				else
				{
					this._weak.Target = target;
				}
				this._tag = tag;
			}

			// Token: 0x060022FD RID: 8957 RVA: 0x000A0F5B File Offset: 0x0009F15B
			public void RemoveTarget()
			{
				this._tag = 0;
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x060022FE RID: 8958 RVA: 0x000A0F64 File Offset: 0x0009F164
			public bool HasTarget
			{
				get
				{
					return this._tag != 0 && this._weak.IsAlive;
				}
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x060022FF RID: 8959 RVA: 0x000A0F7B File Offset: 0x0009F17B
			public int Tag
			{
				get
				{
					return this._tag;
				}
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06002300 RID: 8960 RVA: 0x000A0F83 File Offset: 0x0009F183
			public object Target
			{
				get
				{
					if (this._tag != 0)
					{
						return this._weak.Target;
					}
					return null;
				}
			}

			// Token: 0x04001765 RID: 5989
			private int _tag;

			// Token: 0x04001766 RID: 5990
			private WeakReference _weak;
		}
	}
}
