using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.LookDev
{
	// Token: 0x020000E8 RID: 232
	public interface IDataProvider
	{
		// Token: 0x060006D7 RID: 1751
		void FirstInitScene(StageRuntimeInterface stage);

		// Token: 0x060006D8 RID: 1752
		void UpdateSky(Camera camera, Sky sky, StageRuntimeInterface stage);

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060006D9 RID: 1753
		IEnumerable<string> supportedDebugModes { get; }

		// Token: 0x060006DA RID: 1754
		void UpdateDebugMode(int debugIndex);

		// Token: 0x060006DB RID: 1755
		void GetShadowMask(ref RenderTexture output, StageRuntimeInterface stage);

		// Token: 0x060006DC RID: 1756
		void OnBeginRendering(StageRuntimeInterface stage);

		// Token: 0x060006DD RID: 1757
		void OnEndRendering(StageRuntimeInterface stage);

		// Token: 0x060006DE RID: 1758
		void Cleanup(StageRuntimeInterface SRI);
	}
}
