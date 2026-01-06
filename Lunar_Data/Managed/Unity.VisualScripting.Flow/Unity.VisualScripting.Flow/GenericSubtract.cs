using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000D0 RID: 208
	[UnitCategory("Math/Generic")]
	[UnitTitle("Subtract")]
	public sealed class GenericSubtract : Subtract<object>
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x0000C755 File Offset: 0x0000A955
		public override object Operation(object a, object b)
		{
			return OperatorUtility.Subtract(a, b);
		}
	}
}
