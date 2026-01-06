using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000127 RID: 295
	public struct Vector3MagnitudeComparer : IComparer<Vector3>
	{
		// Token: 0x0600107E RID: 4222 RVA: 0x0004E7F8 File Offset: 0x0004C9F8
		public int Compare(Vector3 x, Vector3 y)
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
