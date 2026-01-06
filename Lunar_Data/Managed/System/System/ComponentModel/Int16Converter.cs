using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit signed integer objects to and from other representations.</summary>
	// Token: 0x020006D5 RID: 1749
	public class Int16Converter : BaseNumberConverter
	{
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060037AD RID: 14253 RVA: 0x000C392F File Offset: 0x000C1B2F
		internal override Type TargetType
		{
			get
			{
				return typeof(short);
			}
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000C393B File Offset: 0x000C1B3B
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt16(value, radix);
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000C3949 File Offset: 0x000C1B49
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return short.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000C3958 File Offset: 0x000C1B58
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((short)value).ToString("G", formatInfo);
		}
	}
}
