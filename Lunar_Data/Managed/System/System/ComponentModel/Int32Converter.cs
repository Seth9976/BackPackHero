using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit signed integer objects to and from other representations.</summary>
	// Token: 0x020006D6 RID: 1750
	public class Int32Converter : BaseNumberConverter
	{
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x000C3979 File Offset: 0x000C1B79
		internal override Type TargetType
		{
			get
			{
				return typeof(int);
			}
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000C3985 File Offset: 0x000C1B85
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt32(value, radix);
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000C3993 File Offset: 0x000C1B93
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return int.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000C39A4 File Offset: 0x000C1BA4
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((int)value).ToString("G", formatInfo);
		}
	}
}
