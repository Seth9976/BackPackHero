using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000075 RID: 117
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct GetHapticCapabilitiesCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0003687F File Offset: 0x00034A7F
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'C', '0');
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0003688E File Offset: 0x00034A8E
		public FourCC typeStatic
		{
			get
			{
				return GetHapticCapabilitiesCommand.Type;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00036895 File Offset: 0x00034A95
		public HapticCapabilities capabilities
		{
			get
			{
				return new HapticCapabilities(this.numChannels, this.frequencyHz, this.maxBufferSize);
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000368B0 File Offset: 0x00034AB0
		public static GetHapticCapabilitiesCommand Create()
		{
			return new GetHapticCapabilitiesCommand
			{
				baseCommand = new InputDeviceCommand(GetHapticCapabilitiesCommand.Type, 20)
			};
		}

		// Token: 0x04000363 RID: 867
		private const int kSize = 20;

		// Token: 0x04000364 RID: 868
		[FieldOffset(0)]
		private InputDeviceCommand baseCommand;

		// Token: 0x04000365 RID: 869
		[FieldOffset(8)]
		public uint numChannels;

		// Token: 0x04000366 RID: 870
		[FieldOffset(12)]
		public uint frequencyHz;

		// Token: 0x04000367 RID: 871
		[FieldOffset(16)]
		public uint maxBufferSize;
	}
}
