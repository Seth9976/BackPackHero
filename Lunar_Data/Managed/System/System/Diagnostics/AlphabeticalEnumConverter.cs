using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x0200024B RID: 587
	internal sealed class AlphabeticalEnumConverter : EnumConverter
	{
		// Token: 0x06001212 RID: 4626 RVA: 0x0004E1E4 File Offset: 0x0004C3E4
		public AlphabeticalEnumConverter(Type type)
			: base(type)
		{
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0004E1ED File Offset: 0x0004C3ED
		[MonoTODO("Create sorted standart values")]
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return base.Values;
		}
	}
}
