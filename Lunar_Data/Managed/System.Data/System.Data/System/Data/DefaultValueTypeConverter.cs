using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x0200008C RID: 140
	internal sealed class DefaultValueTypeConverter : StringConverter
	{
		// Token: 0x060009C2 RID: 2498 RVA: 0x0002AE58 File Offset: 0x00029058
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				if (value == null)
				{
					return "<null>";
				}
				if (value == DBNull.Value)
				{
					return "<DBNull>";
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002AEB0 File Offset: 0x000290B0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value != null && value.GetType() == typeof(string))
			{
				string text = (string)value;
				if (string.Equals(text, "<null>", StringComparison.OrdinalIgnoreCase))
				{
					return null;
				}
				if (string.Equals(text, "<DBNull>", StringComparison.OrdinalIgnoreCase))
				{
					return DBNull.Value;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x04000634 RID: 1588
		private const string NullString = "<null>";

		// Token: 0x04000635 RID: 1589
		private const string DbNullString = "<DBNull>";
	}
}
