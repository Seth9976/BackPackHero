using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from a string.</summary>
	// Token: 0x020006FF RID: 1791
	public class SByteConverter : BaseNumberConverter
	{
		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06003958 RID: 14680 RVA: 0x000C83FC File Offset: 0x000C65FC
		internal override Type TargetType
		{
			get
			{
				return typeof(sbyte);
			}
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000C8408 File Offset: 0x000C6608
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSByte(value, radix);
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000C8416 File Offset: 0x000C6616
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return sbyte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x000C8428 File Offset: 0x000C6628
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((sbyte)value).ToString("G", formatInfo);
		}
	}
}
