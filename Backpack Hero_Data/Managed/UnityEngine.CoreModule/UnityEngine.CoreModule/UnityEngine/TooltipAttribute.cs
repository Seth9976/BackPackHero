using System;

namespace UnityEngine
{
	// Token: 0x020001D7 RID: 471
	[AttributeUsage(32767, Inherited = true, AllowMultiple = false)]
	public class TooltipAttribute : PropertyAttribute
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x00022EDB File Offset: 0x000210DB
		public TooltipAttribute(string tooltip)
		{
			this.tooltip = tooltip;
		}

		// Token: 0x040007AB RID: 1963
		public readonly string tooltip;
	}
}
