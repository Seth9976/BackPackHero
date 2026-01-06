using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D2 RID: 210
	internal struct AttitudeState : IInputStateTypeInfo
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00042013 File Offset: 0x00040213
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('A', 'T', 'T', 'D');
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00042022 File Offset: 0x00040222
		public FourCC format
		{
			get
			{
				return AttitudeState.kFormat;
			}
		}

		// Token: 0x0400052E RID: 1326
		[InputControl(displayName = "Attitude", processors = "CompensateRotation", noisy = true)]
		public Quaternion attitude;
	}
}
