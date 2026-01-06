using System;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000074 RID: 116
	public struct HapticCapabilities
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x00036835 File Offset: 0x00034A35
		public HapticCapabilities(uint numChannels, uint frequencyHz, uint maxBufferSize)
		{
			this.numChannels = numChannels;
			this.frequencyHz = frequencyHz;
			this.maxBufferSize = maxBufferSize;
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0003684C File Offset: 0x00034A4C
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x00036854 File Offset: 0x00034A54
		public uint numChannels { readonly get; private set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0003685D File Offset: 0x00034A5D
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x00036865 File Offset: 0x00034A65
		public uint frequencyHz { readonly get; private set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0003686E File Offset: 0x00034A6E
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x00036876 File Offset: 0x00034A76
		public uint maxBufferSize { readonly get; private set; }
	}
}
