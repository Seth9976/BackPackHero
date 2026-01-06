using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000300 RID: 768
	internal class LinkedPool<T> where T : LinkedPoolItem<T>
	{
		// Token: 0x06001926 RID: 6438 RVA: 0x0006477F File Offset: 0x0006297F
		public LinkedPool(Func<T> createFunc, Action<T> resetAction, int limit = 10000)
		{
			Debug.Assert(createFunc != null);
			this.m_CreateFunc = createFunc;
			Debug.Assert(resetAction != null);
			this.m_ResetAction = resetAction;
			Debug.Assert(limit > 0);
			this.m_Limit = limit;
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x000647BC File Offset: 0x000629BC
		// (set) Token: 0x06001928 RID: 6440 RVA: 0x000647C4 File Offset: 0x000629C4
		public int Count { get; private set; }

		// Token: 0x06001929 RID: 6441 RVA: 0x000647CD File Offset: 0x000629CD
		public void Clear()
		{
			this.m_PoolFirst = default(T);
			this.Count = 0;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x000647E4 File Offset: 0x000629E4
		public T Get()
		{
			T t = this.m_PoolFirst;
			bool flag = this.m_PoolFirst != null;
			if (flag)
			{
				int num = this.Count - 1;
				this.Count = num;
				this.m_PoolFirst = t.poolNext;
				this.m_ResetAction.Invoke(t);
			}
			else
			{
				t = this.m_CreateFunc.Invoke();
			}
			return t;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00064850 File Offset: 0x00062A50
		public void Return(T item)
		{
			bool flag = this.Count < this.m_Limit;
			if (flag)
			{
				item.poolNext = this.m_PoolFirst;
				this.m_PoolFirst = item;
				int num = this.Count + 1;
				this.Count = num;
			}
		}

		// Token: 0x04000ACD RID: 2765
		private readonly Func<T> m_CreateFunc;

		// Token: 0x04000ACE RID: 2766
		private readonly Action<T> m_ResetAction;

		// Token: 0x04000ACF RID: 2767
		private readonly int m_Limit;

		// Token: 0x04000AD0 RID: 2768
		private T m_PoolFirst;
	}
}
