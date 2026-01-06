using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D3 RID: 211
	internal struct LinearAccelerationState : IInputStateTypeInfo
	{
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00042029 File Offset: 0x00040229
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('L', 'A', 'A', 'C');
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00042038 File Offset: 0x00040238
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
