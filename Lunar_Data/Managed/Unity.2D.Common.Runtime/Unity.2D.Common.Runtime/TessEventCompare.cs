using System;
using System.Collections.Generic;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000015 RID: 21
	internal struct TessEventCompare : IComparer<UEvent>
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000057EC File Offset: 0x000039EC
		public int Compare(UEvent a, UEvent b)
		{
			float num = a.a.x - b.a.x;
			if (0f != num)
			{
				if (num <= 0f)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				num = a.a.y - b.a.y;
				if (0f != num)
				{
					if (num <= 0f)
					{
						return -1;
					}
					return 1;
				}
				else
				{
					int num2 = a.type - b.type;
					if (num2 != 0)
					{
						return num2;
					}
					if (a.type != 0)
					{
						float num3 = ModuleHandle.OrientFast(a.a, a.b, b.b);
						if (0f != num3)
						{
							if (num3 <= 0f)
							{
								return -1;
							}
							return 1;
						}
					}
					return a.idx - b.idx;
				}
			}
		}
	}
}
