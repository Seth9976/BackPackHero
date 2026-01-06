using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000C3 RID: 195
	internal sealed class PrimaryKeyTypeConverter : ReferenceConverter
	{
		// Token: 0x06000B9A RID: 2970 RVA: 0x0003411A File Offset: 0x0003231A
		public PrimaryKeyTypeConverter()
			: base(typeof(DataColumn[]))
		{
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0000CC28 File Offset: 0x0000AE28
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0003412C File Offset: 0x0003232C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			return Array.Empty<DataColumn>().GetType().Name;
		}
	}
}
