using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000EF RID: 239
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Add")]
	[Obsolete("Use the new \"Add (Math/Vector 2)\" node instead.")]
	[RenamedFrom("Bolt.Vector2Add")]
	[RenamedFrom("Unity.VisualScripting.Vector2Add")]
	public sealed class DeprecatedVector2Add : Add<Vector2>
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0000D72E File Offset: 0x0000B92E
		protected override Vector2 defaultB
		{
			get
			{
				return Vector2.zero;
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0000D735 File Offset: 0x0000B935
		public override Vector2 Operation(Vector2 a, Vector2 b)
		{
			return a + b;
		}
	}
}
