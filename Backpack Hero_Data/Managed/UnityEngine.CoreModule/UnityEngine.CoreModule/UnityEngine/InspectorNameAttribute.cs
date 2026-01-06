using System;

namespace UnityEngine
{
	// Token: 0x020001D6 RID: 470
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public class InspectorNameAttribute : PropertyAttribute
	{
		// Token: 0x060015CC RID: 5580 RVA: 0x00022ECA File Offset: 0x000210CA
		public InspectorNameAttribute(string displayName)
		{
			this.displayName = displayName;
		}

		// Token: 0x040007AA RID: 1962
		public readonly string displayName;
	}
}
