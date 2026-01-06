using System;

namespace UnityEngine.Pool
{
	// Token: 0x0200037E RID: 894
	public class LinkedPool<T> : IDisposable, IObjectPool<T> where T : class
	{
		// Token: 0x06001E9C RID: 7836 RVA: 0x000318CC File Offset: 0x0002FACC
		public LinkedPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int maxSize = 10000)
		{
			bool flag = createFunc == null;
			if (flag)
			{
				throw new ArgumentNullException("createFunc");
			}
			bool flag2 = maxSize <= 0;
			if (flag2)
			{
				throw new ArgumentException("maxSize", "Max size must be greater than 0");
			}
			this.m_CreateFunc = createFunc;
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
			this.m_ActionOnDestroy = actionOnDestroy;
			this.m_Limit = maxSize;
			this.m_CollectionCheck = collectionCheck;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x0003193D File Offset: 0x0002FB3D
		// (set) Token: 0x06001E9E RID: 7838 RVA: 0x00031945 File Offset: 0x0002FB45
		public int CountInactive { get; private set; }

		// Token: 0x06001E9F RID: 7839 RVA: 0x00031950 File Offset: 0x0002FB50
		public T Get()
		{
			T t = default(T);
			bool flag = this.m_PoolFirst == null;
			if (flag)
			{
				t = this.m_CreateFunc.Invoke();
			}
			else
			{
				LinkedPool<T>.LinkedPoolItem poolFirst = this.m_PoolFirst;
				t = poolFirst.value;
				this.m_PoolFirst = poolFirst.poolNext;
				poolFirst.poolNext = this.m_NextAvailableListItem;
				this.m_NextAvailableListItem = poolFirst;
				this.m_NextAvailableListItem.value = default(T);
				int num = this.CountInactive - 1;
				this.CountInactive = num;
			}
			Action<T> actionOnGet = this.m_ActionOnGet;
			if (actionOnGet != null)
			{
				actionOnGet.Invoke(t);
			}
			return t;
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000319F0 File Offset: 0x0002FBF0
		public PooledObject<T> Get(out T v)
		{
			return new PooledObject<T>(v = this.Get(), this);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00031A14 File Offset: 0x0002FC14
		public void Release(T item)
		{
			Action<T> actionOnRelease = this.m_ActionOnRelease;
			if (actionOnRelease != null)
			{
				actionOnRelease.Invoke(item);
			}
			bool flag = this.CountInactive < this.m_Limit;
			if (flag)
			{
				LinkedPool<T>.LinkedPoolItem linkedPoolItem = this.m_NextAvailableListItem;
				bool flag2 = linkedPoolItem == null;
				if (flag2)
				{
					linkedPoolItem = new LinkedPool<T>.LinkedPoolItem();
				}
				else
				{
					this.m_NextAvailableListItem = linkedPoolItem.poolNext;
				}
				linkedPoolItem.value = item;
				linkedPoolItem.poolNext = this.m_PoolFirst;
				this.m_PoolFirst = linkedPoolItem;
				int num = this.CountInactive + 1;
				this.CountInactive = num;
			}
			else
			{
				Action<T> actionOnDestroy = this.m_ActionOnDestroy;
				if (actionOnDestroy != null)
				{
					actionOnDestroy.Invoke(item);
				}
			}
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00031AB4 File Offset: 0x0002FCB4
		public void Clear()
		{
			bool flag = this.m_ActionOnDestroy != null;
			if (flag)
			{
				for (LinkedPool<T>.LinkedPoolItem linkedPoolItem = this.m_PoolFirst; linkedPoolItem != null; linkedPoolItem = linkedPoolItem.poolNext)
				{
					this.m_ActionOnDestroy.Invoke(linkedPoolItem.value);
				}
			}
			this.m_PoolFirst = null;
			this.m_NextAvailableListItem = null;
			this.CountInactive = 0;
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00031B13 File Offset: 0x0002FD13
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x040009FF RID: 2559
		private readonly Func<T> m_CreateFunc;

		// Token: 0x04000A00 RID: 2560
		private readonly Action<T> m_ActionOnGet;

		// Token: 0x04000A01 RID: 2561
		private readonly Action<T> m_ActionOnRelease;

		// Token: 0x04000A02 RID: 2562
		private readonly Action<T> m_ActionOnDestroy;

		// Token: 0x04000A03 RID: 2563
		private readonly int m_Limit;

		// Token: 0x04000A04 RID: 2564
		internal LinkedPool<T>.LinkedPoolItem m_PoolFirst;

		// Token: 0x04000A05 RID: 2565
		internal LinkedPool<T>.LinkedPoolItem m_NextAvailableListItem;

		// Token: 0x04000A06 RID: 2566
		private bool m_CollectionCheck;

		// Token: 0x0200037F RID: 895
		internal class LinkedPoolItem
		{
			// Token: 0x04000A08 RID: 2568
			internal LinkedPool<T>.LinkedPoolItem poolNext;

			// Token: 0x04000A09 RID: 2569
			internal T value;
		}
	}
}
