using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D1 RID: 209
	internal struct GravityState : IInputStateTypeInfo
	{
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00041FFD File Offset: 0x000401FD
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('G', 'R', 'V', ' ');
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0004200C File Offset: 0x0004020C
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
