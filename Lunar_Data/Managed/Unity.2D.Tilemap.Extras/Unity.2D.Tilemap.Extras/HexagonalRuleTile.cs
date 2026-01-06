using System;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	public class HexagonalRuleTile<T> : HexagonalRuleTile
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public sealed override Type m_NeighborType
		{
			get
			{
				return typeof(T);
			}
		}
	}
}
