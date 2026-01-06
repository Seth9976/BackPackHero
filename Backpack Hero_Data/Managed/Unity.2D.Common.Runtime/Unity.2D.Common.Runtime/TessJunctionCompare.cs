using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000018 RID: 24
	internal struct TessJunctionCompare : IComparer<int2>
	{
		// Token: 0x06000054 RID: 84 RVA: 0x0000591C File Offset: 0x00003B1C
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
