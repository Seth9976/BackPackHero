using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000100 RID: 256
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Subtract")]
	public sealed class Vector2Subtract : Subtract<Vector2>
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0000DA79 File Offset: 0x0000BC79
		protected override Vector2 defaultMinuend
		{
			get
			{
				return Vector2.zero;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0000DA80 File Offset: 0x0000BC80
		protected override Vector2 defaultSubtrahend
		{
			get
			{
				return Vector2.zero;
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000DA87 File Offset: 0x0000BC87
		public override Vector2 Operation(Vector2 a, Vector2 b)
		{
			return a - b;
		}
	}
}
