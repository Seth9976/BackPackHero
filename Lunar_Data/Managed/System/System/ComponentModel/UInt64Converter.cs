using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x0200070E RID: 1806
	public class UInt64Converter : BaseNumberConverter
	{
		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060039A8 RID: 14760 RVA: 0x000C8C55 File Offset: 0x000C6E55
		internal override Type TargetType
		{
			get
			{
				return typeof(ulong);
			}
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000C8C61 File Offset: 0x000C6E61
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt64(value, radix);
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x000C8C6F File Offset: 0x000C6E6F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ulong.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x000C8C80 File Offset: 0x000C6E80
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ulong)value).ToString("G", formatInfo);
		}
	}
}
