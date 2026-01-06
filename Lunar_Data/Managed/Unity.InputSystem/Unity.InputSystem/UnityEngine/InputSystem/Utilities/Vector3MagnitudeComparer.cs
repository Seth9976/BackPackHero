using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000127 RID: 295
	public struct Vector3MagnitudeComparer : IComparer<Vector3>
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x0004E7AC File Offset: 0x0004C9AC
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
