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
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00036843 File Offset: 0x00034A43
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'C', '0');
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00036852 File Offset: 0x00034A52
		public FourCC typeStatic
		{
			get
			{
				return GetHapticCapabilitiesCommand.Type;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00036859 File Offset: 0x00034A59
		public HapticCapabilities capabilities
		{
			get
			{
				return new HapticCapabilities(this.numChannels, this.frequencyHz, this.maxBufferSize);
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00036874 File Offset: 0x00034A74
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
