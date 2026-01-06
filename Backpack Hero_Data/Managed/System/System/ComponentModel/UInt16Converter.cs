using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x0200070C RID: 1804
	public class UInt16Converter : BaseNumberConverter
	{
		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000C8BBD File Offset: 0x000C6DBD
		internal override Type TargetType
		{
			get
			{
				return typeof(ushort);
			}
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x000C8BC9 File Offset: 0x000C6DC9
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt16(value, radix);
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000C8BD7 File Offset: 0x000C6DD7
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ushort.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000C8BE8 File Offset: 0x000C6DE8
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ushort)value).ToString("G", formatInfo);
		}
	}
}
