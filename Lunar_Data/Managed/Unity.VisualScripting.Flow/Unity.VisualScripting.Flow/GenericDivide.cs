using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000CD RID: 205
	[UnitCategory("Math/Generic")]
	[UnitTitle("Divide")]
	public sealed class GenericDivide : Divide<object>
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x0000C722 File Offset: 0x0000A922
		public override object Operation(object a, object b)
		{
			return OperatorUtility.Divide(a, b);
		}
	}
}
