using System;
using System.Collections.Generic;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000013 RID: 19
	internal struct XCompare : IComparer<double>
	{
		// Token: 0x0600004F RID: 79 RVA: 0x0000557D File Offset: 0x0000377D
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
