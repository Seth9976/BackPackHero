using System;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	public class IsometricRuleTile<T> : IsometricRuleTile
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002441 File Offset: 0x00000641
		public sealed override Type m_NeighborType
		{
			get
			{
				return typeof(T);
			}
		}
	}
}
