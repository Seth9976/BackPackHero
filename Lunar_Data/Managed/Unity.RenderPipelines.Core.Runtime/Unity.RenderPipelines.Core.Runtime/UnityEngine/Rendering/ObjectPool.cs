using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.Rendering
{
	// Token: 0x02000059 RID: 89
	public class ObjectPool<T> where T : new()
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000EC3F File Offset: 0x0000CE3F
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000EC47 File Offset: 0x0000CE47
		public int countAll { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000EC50 File Offset: 0x0000CE50
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000EC5F File Offset: 0x0000CE5F
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease, bool collectionCheck = true)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
			this.m_CollectionCheck = collectionCheck;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		public T Get()
		{
			T t;
			if (this.m_Stack.Count == 0)
			{
				t = new T();
				int countAll = this.countAll;
				this.countAll = countAll + 1;
			}
			else
			{
				t = this.m_Stack.Pop();
			}
			if (this.m_ActionOnGet != null)
			{
				this.m_ActionOnGet(t);
			}
			return t;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public ObjectPool<T>.PooledObject Get(out T v)
		{
			return new ObjectPool<T>.PooledObject(v = this.Get(), this);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000ED12 File Offset: 0x0000CF12
		public void Release(T element)
		{
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x040001F6 RID: 502
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x040001F7 RID: 503
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x040001F8 RID: 504
		private readonly UnityAction<T> m_ActionOnRelease;

		// Token: 0x040001F9 RID: 505
		private readonly bool m_CollectionCheck = true;

		// Token: 0x0200013F RID: 319
		public struct PooledObject : IDisposable
		{
			// Token: 0x0600083B RID: 2107 RVA: 0x000230EA File Offset: 0x000212EA
			internal PooledObject(T value, ObjectPool<T> pool)
			{
				this.m_ToReturn = value;
				this.m_Pool = pool;
			}

			// Token: 0x0600083C RID: 2108 RVA: 0x000230FA File Offset: 0x000212FA
			void IDisposable.Dispose()
			{
				this.m_Pool.Release(this.m_ToReturn);
			}

			// Token: 0x04000500 RID: 1280
			private readonly T m_ToReturn;

			// Token: 0x04000501 RID: 1281
			private readonly ObjectPool<T> m_Pool;
		}
	}
}
