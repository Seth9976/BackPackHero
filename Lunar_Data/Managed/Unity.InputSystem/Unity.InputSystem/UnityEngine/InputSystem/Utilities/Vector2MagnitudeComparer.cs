using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000126 RID: 294
	public struct Vector2MagnitudeComparer : IComparer<Vector2>
	{
		// Token: 0x06001078 RID: 4216 RVA: 0x0004E780 File Offset: 0x0004C980
		public int Compare(Vector2 x, Vector2 y)
		{
			float sqrMagnitude = x.sqrMagnitude;
			float sqrMagnitude2 = y.sqrMagnitude;
			if (sqrMagnitude < sqrMagnitude2)
			{
				return -1;
			}
			if (sqrMagnitude > sqrMagnitude2)
			{
				return 1;
			}
			return 0;
		}
	}
}
