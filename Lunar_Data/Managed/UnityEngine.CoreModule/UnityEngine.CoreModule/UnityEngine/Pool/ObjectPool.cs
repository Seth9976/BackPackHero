using System;
using System.Collections.Generic;

namespace UnityEngine.Pool
{
	// Token: 0x02000380 RID: 896
	public class ObjectPool<T> : IDisposable, IObjectPool<T> where T : class
	{
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00031B1D File Offset: 0x0002FD1D
		// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x00031B25 File Offset: 0x0002FD25
		public int CountAll { get; private set; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x00031B30 File Offset: 0x0002FD30
		public int CountActive
		{
			get
			{
				return this.CountAll - this.CountInactive;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x00031B50 File Offset: 0x0002FD50
		public int CountInactive
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00031B70 File Offset: 0x0002FD70
		public ObjectPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
		{
			bool flag = createFunc == null;
			if (flag)
			{
				throw new ArgumentNullException("createFunc");
			}
			bool flag2 = maxSize <= 0;
			if (flag2)
			{
				throw new ArgumentException("Max Size must be greater than 0", "maxSize");
			}
			this.m_List = new List<T>(defaultCapacity);
			this.m_CreateFunc = createFunc;
			this.m_MaxSize = maxSize;
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
			this.m_ActionOnDestroy = actionOnDestroy;
			this.m_CollectionCheck = collectionCheck;
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00031BF0 File Offset: 0x0002FDF0
		public T Get()
		{
			bool flag = this.m_List.Count == 0;
			T t;
			if (flag)
			{
				t = this.m_CreateFunc.Invoke();
				int countAll = this.CountAll;
				this.CountAll = countAll + 1;
			}
			else
			{
				int num = this.m_List.Count - 1;
				t = this.m_List[num];
				this.m_List.RemoveAt(num);
			}
			Action<T> actionOnGet = this.m_ActionOnGet;
			if (actionOnGet != null)
			{
				actionOnGet.Invoke(t);
			}
			return t;
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00031C78 File Offset: 0x0002FE78
		public PooledObject<T> Get(out T v)
		{
			return new PooledObject<T>(v = this.Get(), this);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00031C9C File Offset: 0x0002FE9C
		public void Release(T element)
		{
			Action<T> actionOnRelease = this.m_ActionOnRelease;
			if (actionOnRelease != null)
			{
				actionOnRelease.Invoke(element);
			}
			bool flag = this.CountInactive < this.m_MaxSize;
			if (flag)
			{
				this.m_List.Add(element);
			}
			else
			{
				Action<T> actionOnDestroy = this.m_ActionOnDestroy;
				if (actionOnDestroy != null)
				{
					actionOnDestroy.Invoke(element);
				}
			}
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00031CF8 File Offset: 0x0002FEF8
		public void Clear()
		{
			bool flag = this.m_ActionOnDestroy != null;
			if (flag)
			{
				foreach (T t in this.m_List)
				{
					this.m_ActionOnDestroy.Invoke(t);
				}
			}
			this.m_List.Clear();
			this.CountAll = 0;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x00031D7C File Offset: 0x0002FF7C
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x04000A0A RID: 2570
		internal readonly List<T> m_List;

		// Token: 0x04000A0B RID: 2571
		private readonly Func<T> m_CreateFunc;

		// Token: 0x04000A0C RID: 2572
		private readonly Action<T> m_ActionOnGet;

		// Token: 0x04000A0D RID: 2573
		private readonly Action<T> m_ActionOnRelease;

		// Token: 0x04000A0E RID: 2574
		private readonly Action<T> m_ActionOnDestroy;

		// Token: 0x04000A0F RID: 2575
		private readonly int m_MaxSize;

		// Token: 0x04000A10 RID: 2576
		internal bool m_CollectionCheck;
	}
}
