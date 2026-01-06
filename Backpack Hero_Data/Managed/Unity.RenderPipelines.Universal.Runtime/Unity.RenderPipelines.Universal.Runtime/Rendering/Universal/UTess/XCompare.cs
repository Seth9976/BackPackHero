using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x02000128 RID: 296
	internal struct XCompare : IComparer<double>
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x0003C925 File Offset: 0x0003AB25
		public int Compare(double a, double b)
		{
			if (a >= b)
			{
				return 1;
			}
			return -1;
		}
	}
}
