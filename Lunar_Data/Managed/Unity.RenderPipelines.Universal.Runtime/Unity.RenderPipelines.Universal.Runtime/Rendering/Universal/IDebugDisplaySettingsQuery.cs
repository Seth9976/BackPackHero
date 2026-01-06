using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200005A RID: 90
	public interface IDebugDisplaySettingsQuery
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600034C RID: 844
		bool AreAnySettingsActive { get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600034D RID: 845
		bool IsPostProcessingAllowed { get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600034E RID: 846
		bool IsLightingActive { get; }

		// Token: 0x0600034F RID: 847
		bool TryGetScreenClearColor(ref Color color);
	}
}
