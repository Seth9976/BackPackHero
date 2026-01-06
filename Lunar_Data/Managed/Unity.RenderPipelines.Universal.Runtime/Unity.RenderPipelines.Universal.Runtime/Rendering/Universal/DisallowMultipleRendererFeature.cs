using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B0 RID: 176
	[AttributeUsage(AttributeTargets.Class)]
	public class DisallowMultipleRendererFeature : Attribute
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0001E837 File Offset: 0x0001CA37
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0001E82E File Offset: 0x0001CA2E
		public string customTitle { get; private set; }

		// Token: 0x0600054C RID: 1356 RVA: 0x0001E83F File Offset: 0x0001CA3F
		public DisallowMultipleRendererFeature(string customTitle = null)
		{
			this.customTitle = customTitle;
		}
	}
}
