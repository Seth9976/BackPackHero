using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000111 RID: 273
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Per Second")]
	public sealed class Vector3PerSecond : PerSecond<Vector3>
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x0000DDF9 File Offset: 0x0000BFF9
		public override Vector3 Operation(Vector3 input)
		{
			return input * Time.deltaTime;
		}
	}
}
