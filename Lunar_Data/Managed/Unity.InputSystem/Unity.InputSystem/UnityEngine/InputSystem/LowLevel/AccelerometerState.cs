using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000CF RID: 207
	internal struct AccelerometerState : IInputStateTypeInfo
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00041F89 File Offset: 0x00040189
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('A', 'C', 'C', 'L');
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00041F98 File Offset: 0x00040198
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
