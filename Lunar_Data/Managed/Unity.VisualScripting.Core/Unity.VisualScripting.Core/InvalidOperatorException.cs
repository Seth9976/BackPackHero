using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000E4 RID: 228
	public sealed class InvalidOperatorException : OperatorException
	{
		// Token: 0x0600062F RID: 1583 RVA: 0x00017628 File Offset: 0x00015828
		public InvalidOperatorException(string symbol, Type type)
			: base(string.Concat(new string[]
			{
				"Operator '",
				symbol,
				"' cannot be applied to operand of type '",
				((type != null) ? type.ToString() : null) ?? "null",
				"'."
			}))
		{
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001767C File Offset: 0x0001587C
		public InvalidOperatorException(string symbol, Type leftType, Type rightType)
			: base(string.Concat(new string[]
			{
				"Operator '",
				symbol,
				"' cannot be applied to operands of type '",
				((leftType != null) ? leftType.ToString() : null) ?? "null",
				"' and '",
				((rightType != null) ? rightType.ToString() : null) ?? "null",
				"'."
			}))
		{
		}
	}
}
