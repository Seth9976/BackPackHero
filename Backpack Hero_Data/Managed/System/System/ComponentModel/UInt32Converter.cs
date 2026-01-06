using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x0200070D RID: 1805
	public class UInt32Converter : BaseNumberConverter
	{
		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060039A3 RID: 14755 RVA: 0x000C8C09 File Offset: 0x000C6E09
		internal override Type TargetType
		{
			get
			{
				return typeof(uint);
			}
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x000C8C15 File Offset: 0x000C6E15
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt32(value, radix);
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x000C8C23 File Offset: 0x000C6E23
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return uint.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x000C8C34 File Offset: 0x000C6E34
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((uint)value).ToString("G", formatInfo);
		}
	}
}
