using System;
using System.Threading;

namespace Unity.Collections
{
	// Token: 0x020000E4 RID: 228
	internal struct Spinner
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x00019E09 File Offset: 0x00018009
		public void Lock()
		{
			while (Interlocked.CompareExchange(ref this.m_value, 1, 0) != 0)
			{
			}
			Interlocked.MemoryBarrier();
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00019E1F File Offset: 0x0001801F
		public void Unlock()
		{
			Interlocked.MemoryBarrier();
			while (1 != Interlocked.CompareExchange(ref this.m_value, 0, 1))
			{
			}
		}

		// Token: 0x040002D8 RID: 728
		private int m_value;
	}
}
