using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000112 RID: 274
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Project")]
	public sealed class Vector3Project : Project<Vector3>
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x0000DE0E File Offset: 0x0000C00E
		public override Vector3 Operation(Vector3 a, Vector3 b)
		{
			return Vector3.Project(a, b);
		}
	}
}
