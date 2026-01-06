using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B9 RID: 185
	[StructLayout(LayoutKind.Explicit, Size = 12)]
	internal struct QuerySamplingFrequencyCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00041ABC File Offset: 0x0003FCBC
		public static FourCC Type
		{
			get
			{
				return new FourCC('S', 'M', 'P', 'L');
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00041ACB File Offset: 0x0003FCCB
		public FourCC typeStatic
		{
			get
			{
				return QuerySamplingFrequencyCommand.Type;
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00041AD4 File Offset: 0x0003FCD4
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
