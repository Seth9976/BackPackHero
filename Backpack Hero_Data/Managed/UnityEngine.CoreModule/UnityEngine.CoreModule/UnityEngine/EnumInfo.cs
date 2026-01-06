using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000205 RID: 517
	internal class EnumInfo
	{
		// Token: 0x060016B6 RID: 5814 RVA: 0x000249F8 File Offset: 0x00022BF8
		[UsedByNativeCode]
		internal static EnumInfo CreateEnumInfoFromNativeEnum(string[] names, int[] values, string[] annotations, bool isFlags)
		{
			return new EnumInfo
			{
				names = names,
				values = values,
				annotations = annotations,
				isFlags = isFlags
			};
		}

		// Token: 0x040007EE RID: 2030
		public string[] names;

		// Token: 0x040007EF RID: 2031
		public int[] values;

		// Token: 0x040007F0 RID: 2032
		public string[] annotations;

		// Token: 0x040007F1 RID: 2033
		public bool isFlags;
	}
}
