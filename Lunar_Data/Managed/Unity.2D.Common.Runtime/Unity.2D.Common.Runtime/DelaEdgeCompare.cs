using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000019 RID: 25
	internal struct DelaEdgeCompare : IComparer<int4>
	{
		// Token: 0x06000055 RID: 85 RVA: 0x0000594C File Offset: 0x00003B4C
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
