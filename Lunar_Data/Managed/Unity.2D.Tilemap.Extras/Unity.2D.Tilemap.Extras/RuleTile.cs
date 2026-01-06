using System;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	public class RuleTile<T> : RuleTile
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000245D File Offset: 0x0000065D
		public sealed override Type m_NeighborType
		{
			get
			{
				return typeof(T);
			}
		}
	}
}
