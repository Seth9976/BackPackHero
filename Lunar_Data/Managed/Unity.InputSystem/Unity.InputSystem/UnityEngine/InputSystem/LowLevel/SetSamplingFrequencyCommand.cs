using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BE RID: 190
	[StructLayout(LayoutKind.Explicit, Size = 12)]
	public struct SetSamplingFrequencyCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00041C3D File Offset: 0x0003FE3D
		public static FourCC Type
		{
			get
			{
				return new FourCC('S', 'S', 'P', 'L');
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00041C4C File Offset: 0x0003FE4C
		public FourCC typeStatic
		{
			get
			{
				return SetSamplingFrequencyCommand.Type;
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00041C54 File Offset: 0x0003FE54
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
