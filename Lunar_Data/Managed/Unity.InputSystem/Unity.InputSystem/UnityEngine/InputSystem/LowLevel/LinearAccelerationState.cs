using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D3 RID: 211
	internal struct LinearAccelerationState : IInputStateTypeInfo
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00041FE1 File Offset: 0x000401E1
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('L', 'A', 'A', 'C');
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00041FF0 File Offset: 0x000401F0
		public FourCC format
		{
			get
			{
				return LinearAccelerationState.kFormat;
			}
		}

		// Token: 0x0400052F RID: 1327
		[InputControl(displayName = "Acceleration", processors = "CompensateDirection", noisy = true)]
		public Vector3 acceleration;
	}
}
