using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B9 RID: 185
	[StructLayout(LayoutKind.Explicit, Size = 12)]
	internal struct QuerySamplingFrequencyCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00041B04 File Offset: 0x0003FD04
		public static FourCC Type
		{
			get
			{
				return new FourCC('S', 'M', 'P', 'L');
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00041B13 File Offset: 0x0003FD13
		public FourCC typeStatic
		{
			get
			{
				return QuerySamplingFrequencyCommand.Type;
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00041B1C File Offset: 0x0003FD1C
		public static QuerySamplingFrequencyCommand Create()
		{
			return new QuerySamplingFrequencyCommand
			{
				baseCommand = new InputDeviceCommand(QuerySamplingFrequencyCommand.Type, 12)
			};
		}

		// Token: 0x040004CF RID: 1231
		internal const int kSize = 12;

		// Token: 0x040004D0 RID: 1232
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004D1 RID: 1233
		[FieldOffset(8)]
		public float frequency;
	}
}
