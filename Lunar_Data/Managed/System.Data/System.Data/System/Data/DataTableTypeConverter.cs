using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000080 RID: 128
	internal sealed class DataTableTypeConverter : ReferenceConverter
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x0002818F File Offset: 0x0002638F
		public DataTableTypeConverter()
			: base(typeof(DataTable))
		{
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
