using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D1 RID: 209
	internal struct GravityState : IInputStateTypeInfo
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00041FB5 File Offset: 0x000401B5
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('G', 'R', 'V', ' ');
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00041FC4 File Offset: 0x000401C4
		public FourCC format
		{
			get
			{
				return GravityState.kFormat;
			}
		}

		// Token: 0x0400052D RID: 1325
		[InputControl(displayName = "Gravity", processors = "CompensateDirection", noisy = true)]
		public Vector3 gravity;
	}
}
