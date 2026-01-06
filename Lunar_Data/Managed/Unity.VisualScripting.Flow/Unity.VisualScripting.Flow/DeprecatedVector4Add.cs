using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000116 RID: 278
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Add")]
	[Obsolete("Use the new \"Add (Math/Vector 4)\" instead.")]
	[RenamedFrom("Bolt.Vector4Add")]
	[RenamedFrom("Unity.VisualScripting.Vector4Add")]
	public sealed class DeprecatedVector4Add : Add<Vector4>
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0000DF28 File Offset: 0x0000C128
		protected override Vector4 defaultB
		{
			get
			{
				return Vector4.zero;
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000DF2F File Offset: 0x0000C12F
		public override Vector4 Operation(Vector4 a, Vector4 b)
		{
			return a + b;
		}
	}
}
