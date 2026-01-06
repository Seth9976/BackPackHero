using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000016 RID: 22
	internal class MyIntersectNodeSort : IComparer<IntersectNode>
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00003204 File Offset: 0x00001404
		public int Compare(IntersectNode node1, IntersectNode node2)
		{
			long num = node2.Pt.Y - node1.Pt.Y;
			if (num > 0L)
			{
				return 1;
			}
			if (num < 0L)
			{
				return -1;
			}
			return 0;
		}
	}
}
