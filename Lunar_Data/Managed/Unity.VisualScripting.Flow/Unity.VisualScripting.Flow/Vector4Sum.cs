using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000126 RID: 294
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Add")]
	public sealed class Vector4Sum : Sum<Vector4>, IDefaultValue<Vector4>
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0000E308 File Offset: 0x0000C508
		[DoNotSerialize]
		public Vector4 defaultValue
		{
			get
			{
				return Vector4.zero;
			}
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0000E30F File Offset: 0x0000C50F
		public override Vector4 Operation(Vector4 a, Vector4 b)
		{
			return a + b;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0000E318 File Offset: 0x0000C518
		public override Vector4 Operation(IEnumerable<Vector4> values)
		{
			Vector4 vector = Vector4.zero;
			foreach (Vector4 vector2 in values)
			{
				vector += vector2;
			}
			return vector;
		}
	}
}
