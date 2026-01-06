using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000FA RID: 250
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Move Towards")]
	public sealed class Vector2MoveTowards : MoveTowards<Vector2>
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0000D987 File Offset: 0x0000BB87
		protected override Vector2 defaultCurrent
		{
			get
			{
				return Vector2.zero;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0000D98E File Offset: 0x0000BB8E
		protected override Vector2 defaultTarget
		{
			get
			{
				return Vector2.one;
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0000D995 File Offset: 0x0000BB95
		public override Vector2 Operation(Vector2 current, Vector2 target, float maxDelta)
		{
			return Vector2.MoveTowards(current, target, maxDelta);
		}
	}
}
