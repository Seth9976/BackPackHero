using System;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000071 RID: 113
	public struct BufferedRumble
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00036727 File Offset: 0x00034927
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0003672F File Offset: 0x0003492F
		public HapticCapabilities capabilities { readonly get; private set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00036738 File Offset: 0x00034938
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x00036740 File Offset: 0x00034940
		private InputDevice device { readonly get; set; }

		// Token: 0x06000A05 RID: 2565 RVA: 0x0003674C File Offset: 0x0003494C
		public BufferedRumble(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			this.device = device;
			GetHapticCapabilitiesCommand getHapticCapabilitiesCommand = GetHapticCapabilitiesCommand.Create();
			device.ExecuteCommand<GetHapticCapabilitiesCommand>(ref getHapticCapabilitiesCommand);
			this.capabilities = getHapticCapabilitiesCommand.capabilities;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0003678C File Offset: 0x0003498C
		public void EnqueueRumble(byte[] samples)
		{
			SendBufferedHapticCommand sendBufferedHapticCommand = SendBufferedHapticCommand.Create(samples);
			this.device.ExecuteCommand<SendBufferedHapticCommand>(ref sendBufferedHapticCommand);
		}
	}
}
