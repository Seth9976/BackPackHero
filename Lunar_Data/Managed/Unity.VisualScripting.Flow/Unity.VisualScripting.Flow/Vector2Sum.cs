using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000101 RID: 257
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Add")]
	public sealed class Vector2Sum : Sum<Vector2>, IDefaultValue<Vector2>
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0000DA98 File Offset: 0x0000BC98
		[DoNotSerialize]
		public Vector2 defaultValue
		{
			get
			{
				return Vector2.zero;
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000DA9F File Offset: 0x0000BC9F
		public override Vector2 Operation(Vector2 a, Vector2 b)
		{
			return a + b;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000DAA8 File Offset: 0x0000BCA8
		public override Vector2 Operation(IEnumerable<Vector2> values)
		{
			Vector2 vector = Vector2.zero;
			foreach (Vector2 vector2 in values)
			{
				vector += vector2;
			}
			return vector;
		}
	}
}
