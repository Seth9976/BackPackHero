using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x0200012B RID: 299
	internal struct TessEdgeCompare : IComparer<int2>
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0003CC50 File Offset: 0x0003AE50
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
