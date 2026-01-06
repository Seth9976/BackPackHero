using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x0200012D RID: 301
	internal struct TessJunctionCompare : IComparer<int2>
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x0003CCC4 File Offset: 0x0003AEC4
		public int Compare(int2 a, int2 b)
		{
			int num = a.x - b.x;
			if (num != 0)
			{
				return num;
			}
			return a.y - b.y;
		}
	}
}
