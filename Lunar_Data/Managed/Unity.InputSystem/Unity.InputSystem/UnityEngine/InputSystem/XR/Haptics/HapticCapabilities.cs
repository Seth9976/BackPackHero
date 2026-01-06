using System;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000074 RID: 116
	public struct HapticCapabilities
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x000367F9 File Offset: 0x000349F9
		public HapticCapabilities(uint numChannels, uint frequencyHz, uint maxBufferSize)
		{
			this.numChannels = numChannels;
			this.frequencyHz = frequencyHz;
			this.maxBufferSize = maxBufferSize;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00036810 File Offset: 0x00034A10
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x00036818 File Offset: 0x00034A18
		public uint numChannels { readonly get; private set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00036821 File Offset: 0x00034A21
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x00036829 File Offset: 0x00034A29
		public uint frequencyHz { readonly get; private set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00036832 File Offset: 0x00034A32
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x0003683A File Offset: 0x00034A3A
		public uint maxBufferSize { readonly get; private set; }
	}
}
