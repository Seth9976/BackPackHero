using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000101 RID: 257
	internal class SortPrePunctualLight : IComparer<DeferredTiler.PrePunctualLight>
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x0002F296 File Offset: 0x0002D496
		public int Compare(DeferredTiler.PrePunctualLight a, DeferredTiler.PrePunctualLight b)
		{
			if (a.minDist < b.minDist)
			{
				return -1;
			}
			if (a.minDist > b.minDist)
			{
				return 1;
			}
			return 0;
		}
	}
}
