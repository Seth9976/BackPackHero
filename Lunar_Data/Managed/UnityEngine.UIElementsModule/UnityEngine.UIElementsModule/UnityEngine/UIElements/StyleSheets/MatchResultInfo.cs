using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200035E RID: 862
	internal struct MatchResultInfo
	{
		// Token: 0x06001BBA RID: 7098 RVA: 0x00080726 File Offset: 0x0007E926
		public MatchResultInfo(bool success, PseudoStates triggerPseudoMask, PseudoStates dependencyPseudoMask)
		{
			this.success = success;
			this.triggerPseudoMask = triggerPseudoMask;
			this.dependencyPseudoMask = dependencyPseudoMask;
		}

		// Token: 0x04000DBC RID: 3516
		public readonly bool success;

		// Token: 0x04000DBD RID: 3517
		public readonly PseudoStates triggerPseudoMask;

		// Token: 0x04000DBE RID: 3518
		public readonly PseudoStates dependencyPseudoMask;
	}
}
