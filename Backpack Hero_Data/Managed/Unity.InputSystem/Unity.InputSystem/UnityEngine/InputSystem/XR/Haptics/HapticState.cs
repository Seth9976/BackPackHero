using System;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000072 RID: 114
	public struct HapticState
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x000367AE File Offset: 0x000349AE
		public HapticState(uint samplesQueued, uint samplesAvailable)
		{
			this.samplesQueued = samplesQueued;
			this.samplesAvailable = samplesAvailable;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x000367BE File Offset: 0x000349BE
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x000367C6 File Offset: 0x000349C6
		public uint samplesQueued { readonly get; private set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x000367CF File Offset: 0x000349CF
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x000367D7 File Offset: 0x000349D7
		public uint samplesAvailable { readonly get; private set; }
	}
}
