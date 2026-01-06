using System;

namespace UnityEngine.Pool
{
	// Token: 0x02000381 RID: 897
	public struct PooledObject<T> : IDisposable where T : class
	{
		// Token: 0x06001EAF RID: 7855 RVA: 0x00031D86 File Offset: 0x0002FF86
		internal PooledObject(T value, IObjectPool<T> pool)
		{
			this.m_ToReturn = value;
			this.m_Pool = pool;
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00031D97 File Offset: 0x0002FF97
		void IDisposable.Dispose()
		{
			this.m_Pool.Release(this.m_ToReturn);
		}

		// Token: 0x04000A12 RID: 2578
		private readonly T m_ToReturn;

		// Token: 0x04000A13 RID: 2579
		private readonly IObjectPool<T> m_Pool;
	}
}
