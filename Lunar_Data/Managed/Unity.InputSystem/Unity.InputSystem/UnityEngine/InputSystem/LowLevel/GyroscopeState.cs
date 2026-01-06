using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D0 RID: 208
	internal struct GyroscopeState : IInputStateTypeInfo
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00041F9F File Offset: 0x0004019F
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('G', 'Y', 'R', 'O');
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00041FAE File Offset: 0x000401AE
		public FourCC format
		{
			get
			{
				return GyroscopeState.kFormat;
			}
		}

		// Token: 0x0400052C RID: 1324
		[InputControl(displayName = "Angular Velocity", processors = "CompensateDirection", noisy = true)]
		public Vector3 angularVelocity;
	}
}
