using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000DC RID: 220
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Add")]
	[Obsolete("Use the new \"Add (Math/Scalar)\" node instead.")]
	[RenamedFrom("Bolt.ScalarAdd")]
	[RenamedFrom("Unity.VisualScripting.ScalarAdd")]
	public sealed class DeprecatedScalarAdd : Add<float>
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0000D173 File Offset: 0x0000B373
		protected override float defaultB
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000D17A File Offset: 0x0000B37A
		public override float Operation(float a, float b)
		{
			return a + b;
		}
	}
}
