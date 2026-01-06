using System;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000072 RID: 114
	public struct HapticState
	{
		// Token: 0x06000A05 RID: 2565 RVA: 0x00036772 File Offset: 0x00034972
		public HapticState(uint samplesQueued, uint samplesAvailable)
		{
			this.samplesQueued = samplesQueued;
			this.samplesAvailable = samplesAvailable;
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00036782 File Offset: 0x00034982
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x0003678A File Offset: 0x0003498A
		public uint samplesQueued { readonly get; private set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00036793 File Offset: 0x00034993
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0003679B File Offset: 0x0003499B
		public uint samplesAvailable { readonly get; private set; }
	}
}
