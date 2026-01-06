using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D2 RID: 210
	internal struct AttitudeState : IInputStateTypeInfo
	{
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00041FCB File Offset: 0x000401CB
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('A', 'T', 'T', 'D');
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00041FDA File Offset: 0x000401DA
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
