using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D0 RID: 208
	internal struct GyroscopeState : IInputStateTypeInfo
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00041FE7 File Offset: 0x000401E7
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('G', 'Y', 'R', 'O');
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00041FF6 File Offset: 0x000401F6
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
