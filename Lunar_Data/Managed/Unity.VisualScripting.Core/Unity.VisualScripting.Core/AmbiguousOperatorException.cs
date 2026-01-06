using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000D8 RID: 216
	public sealed class AmbiguousOperatorException : OperatorException
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x00010B34 File Offset: 0x0000ED34
		public AmbiguousOperatorException(string symbol, Type leftType, Type rightType)
			: base(string.Concat(new string[]
			{
				"Ambiguous use of operator '",
				symbol,
				"' between types '",
				((leftType != null) ? leftType.ToString() : null) ?? "null",
				"' and '",
				((rightType != null) ? rightType.ToString() : null) ?? "null",
				"'."
			}))
		{
		}
	}
}
