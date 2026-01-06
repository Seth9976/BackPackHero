using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BE RID: 190
	[StructLayout(LayoutKind.Explicit, Size = 12)]
	public struct SetSamplingFrequencyCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00041C85 File Offset: 0x0003FE85
		public static FourCC Type
		{
			get
			{
				return new FourCC('S', 'S', 'P', 'L');
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00041C94 File Offset: 0x0003FE94
		public FourCC typeStatic
		{
			get
			{
				return SetSamplingFrequencyCommand.Type;
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00041C9C File Offset: 0x0003FE9C
		public static SetSamplingFrequencyCommand Create(float frequency)
		{
			return new SetSamplingFrequencyCommand
			{
				baseCommand = new InputDeviceCommand(SetSamplingFrequencyCommand.Type, 12),
				frequency = frequency
			};
		}

		// Token: 0x040004DD RID: 1245
		internal const int kSize = 12;

		// Token: 0x040004DE RID: 1246
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004DF RID: 1247
		[FieldOffset(8)]
		public float frequency;
	}
}
