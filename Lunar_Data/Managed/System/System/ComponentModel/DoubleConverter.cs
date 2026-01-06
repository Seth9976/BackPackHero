using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert double-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x020006B9 RID: 1721
	public class DoubleConverter : BaseNumberConverter
	{
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x00003062 File Offset: 0x00001262
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000C2D69 File Offset: 0x000C0F69
		internal override Type TargetType
		{
			get
			{
				return typeof(double);
			}
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000C2D75 File Offset: 0x000C0F75
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDouble(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x000C2D87 File Offset: 0x000C0F87
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return double.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000C2D9C File Offset: 0x000C0F9C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((double)value).ToString("R", formatInfo);
		}
	}
}
