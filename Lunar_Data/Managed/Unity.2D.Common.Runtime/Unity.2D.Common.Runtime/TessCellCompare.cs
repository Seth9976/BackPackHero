using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000017 RID: 23
	internal struct TessCellCompare : IComparer<int3>
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000058D8 File Offset: 0x00003AD8
		public int Compare(int3 a, int3 b)
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
			return a.z - b.z;
		}
	}
}
