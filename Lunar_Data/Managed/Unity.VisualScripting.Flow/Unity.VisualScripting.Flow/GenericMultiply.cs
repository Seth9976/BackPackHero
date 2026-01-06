using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000CF RID: 207
	[UnitCategory("Math/Generic")]
	[UnitTitle("Multiply")]
	public sealed class GenericMultiply : Multiply<object>
	{
		// Token: 0x0600063A RID: 1594 RVA: 0x0000C744 File Offset: 0x0000A944
		public override object Operation(object a, object b)
		{
			return OperatorUtility.Multiply(a, b);
		}
	}
}
