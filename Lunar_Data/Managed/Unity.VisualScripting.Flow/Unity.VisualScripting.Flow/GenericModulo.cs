using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000CE RID: 206
	[UnitCategory("Math/Generic")]
	[UnitTitle("Modulo")]
	public sealed class GenericModulo : Modulo<object>
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x0000C733 File Offset: 0x0000A933
		public override object Operation(object a, object b)
		{
			return OperatorUtility.Modulo(a, b);
		}
	}
}
