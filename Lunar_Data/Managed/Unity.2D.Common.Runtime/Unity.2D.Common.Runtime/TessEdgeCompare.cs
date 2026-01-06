using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000016 RID: 22
	internal struct TessEdgeCompare : IComparer<int2>
	{
		// Token: 0x06000052 RID: 82 RVA: 0x000058A8 File Offset: 0x00003AA8
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
