using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert single-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x02000701 RID: 1793
	public class SingleConverter : BaseNumberConverter
	{
		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06003962 RID: 14690 RVA: 0x00003062 File Offset: 0x00001262
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06003963 RID: 14691 RVA: 0x000C84BB File Offset: 0x000C66BB
		internal override Type TargetType
		{
			get
			{
				return typeof(float);
			}
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000C84C7 File Offset: 0x000C66C7
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSingle(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x000C84D9 File Offset: 0x000C66D9
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return float.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000C84EC File Offset: 0x000C66EC
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((float)value).ToString("R", formatInfo);
		}
	}
}
