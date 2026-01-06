using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B3 RID: 179
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	public struct QueryCanRunInBackground : IInputDeviceCommandInfo
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000417AE File Offset: 0x0003F9AE
		public static FourCC Type
		{
			get
			{
				return new FourCC('Q', 'R', 'I', 'B');
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x000417BD File Offset: 0x0003F9BD
		public FourCC typeStatic
		{
			get
			{
				return QueryCanRunInBackground.Type;
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000417C4 File Offset: 0x0003F9C4
		public static QueryCanRunInBackground Create()
		{
			return new QueryCanRunInBackground
			{
				baseCommand = new InputDeviceCommand(QueryCanRunInBackground.Type, 9),
				canRunInBackground = false
			};
		}

		// Token: 0x040004B7 RID: 1207
		internal const int kSize = 9;

		// Token: 0x040004B8 RID: 1208
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004B9 RID: 1209
		[FieldOffset(8)]
		public bool canRunInBackground;
	}
}
