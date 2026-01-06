using System;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000071 RID: 113
	public struct BufferedRumble
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x000366EB File Offset: 0x000348EB
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x000366F3 File Offset: 0x000348F3
		public HapticCapabilities capabilities { readonly get; private set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x000366FC File Offset: 0x000348FC
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x00036704 File Offset: 0x00034904
		private InputDevice device { readonly get; set; }

		// Token: 0x06000A03 RID: 2563 RVA: 0x00036710 File Offset: 0x00034910
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

		// Token: 0x06000A04 RID: 2564 RVA: 0x00036750 File Offset: 0x00034950
		public void EnqueueRumble(byte[] samples)
		{
			SendBufferedHapticCommand sendBufferedHapticCommand = SendBufferedHapticCommand.Create(samples);
			this.device.ExecuteCommand<SendBufferedHapticCommand>(ref sendBufferedHapticCommand);
		}
	}
}
