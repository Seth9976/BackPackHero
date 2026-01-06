using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x0200069C RID: 1692
	public class ByteConverter : BaseNumberConverter
	{
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x0600364D RID: 13901 RVA: 0x000C0552 File Offset: 0x000BE752
		internal override Type TargetType
		{
			get
			{
				return typeof(byte);
			}
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000C055E File Offset: 0x000BE75E
		internal override object FromString(string value, int radix)
		{
			return Convert.ToByte(value, radix);
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000C056C File Offset: 0x000BE76C
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return byte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000C057C File Offset: 0x000BE77C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((byte)value).ToString("G", formatInfo);
		}
	}
}
