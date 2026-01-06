using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x0200012E RID: 302
	internal struct DelaEdgeCompare : IComparer<int4>
	{
		// Token: 0x06000918 RID: 2328 RVA: 0x0003CCF4 File Offset: 0x0003AEF4
		public int Compare(int4 a, int4 b)
		{
			int num = a.x - b.x;
			if (num != 0)
			{
				return num;
			}
			num = a.y - b.y;
			if (num != 0)
			{
				return num;
			}
			num = a.z - b.z;
			if (num != 0)
			{
				return num;
			}
			return a.w - b.w;
		}
	}
}
