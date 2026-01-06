using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000115 RID: 277
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Add")]
	public sealed class Vector3Sum : Sum<Vector3>, IDefaultValue<Vector3>
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0000DEBE File Offset: 0x0000C0BE
		[DoNotSerialize]
		public Vector3 defaultValue
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000DEC5 File Offset: 0x0000C0C5
		public override Vector3 Operation(Vector3 a, Vector3 b)
		{
			return a + b;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000DED0 File Offset: 0x0000C0D0
		public override Vector3 Operation(IEnumerable<Vector3> values)
		{
			Vector3 vector = Vector3.zero;
			foreach (Vector3 vector2 in values)
			{
				vector += vector2;
			}
			return vector;
		}
	}
}
