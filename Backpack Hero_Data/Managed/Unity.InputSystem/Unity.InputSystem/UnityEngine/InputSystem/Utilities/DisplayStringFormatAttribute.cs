using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012A RID: 298
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class DisplayStringFormatAttribute : Attribute
	{
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x0004ED94 File Offset: 0x0004CF94
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x0004ED9C File Offset: 0x0004CF9C
		public string formatString { get; set; }

		// Token: 0x0600108C RID: 4236 RVA: 0x0004EDA5 File Offset: 0x0004CFA5
		public DisplayStringFormatAttribute(string formatString)
		{
			this.formatString = formatString;
		}
	}
}
