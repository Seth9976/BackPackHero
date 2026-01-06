using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000CF RID: 207
	internal struct AccelerometerState : IInputStateTypeInfo
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00041FD1 File Offset: 0x000401D1
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('A', 'C', 'C', 'L');
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00041FE0 File Offset: 0x000401E0
		public FourCC format
		{
			get
			{
				return AccelerometerState.kFormat;
			}
		}

		// Token: 0x0400052B RID: 1323
		[InputControl(displayName = "Acceleration", processors = "CompensateDirection", noisy = true)]
		public Vector3 acceleration;
	}
}
