using System;

namespace UnityEngine
{
	// Token: 0x020001D5 RID: 469
	[AttributeUsage(256, Inherited = true, AllowMultiple = true)]
	public class ContextMenuItemAttribute : PropertyAttribute
	{
		// Token: 0x060015CB RID: 5579 RVA: 0x00022EB2 File Offset: 0x000210B2
		public ContextMenuItemAttribute(string name, string function)
		{
			this.name = name;
			this.function = function;
		}

		// Token: 0x040007A8 RID: 1960
		public readonly string name;

		// Token: 0x040007A9 RID: 1961
		public readonly string function;
	}
}
