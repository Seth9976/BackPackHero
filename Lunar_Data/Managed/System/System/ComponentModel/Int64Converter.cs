using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit signed integer objects to and from various other representations.</summary>
	// Token: 0x020006D7 RID: 1751
	public class Int64Converter : BaseNumberConverter
	{
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x060037B7 RID: 14263 RVA: 0x000C39C5 File Offset: 0x000C1BC5
		internal override Type TargetType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000C39D1 File Offset: 0x000C1BD1
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt64(value, radix);
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000C39DF File Offset: 0x000C1BDF
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return long.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000C39F0 File Offset: 0x000C1BF0
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((long)value).ToString("G", formatInfo);
		}
	}
}
