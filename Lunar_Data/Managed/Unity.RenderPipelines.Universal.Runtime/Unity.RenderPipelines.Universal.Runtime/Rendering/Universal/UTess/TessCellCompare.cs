using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x0200012C RID: 300
	internal struct TessCellCompare : IComparer<int3>
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x0003CC80 File Offset: 0x0003AE80
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
