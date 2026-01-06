using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B3 RID: 179
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	public struct QueryCanRunInBackground : IInputDeviceCommandInfo
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00041766 File Offset: 0x0003F966
		public static FourCC Type
		{
			get
			{
				return new FourCC('Q', 'R', 'I', 'B');
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00041775 File Offset: 0x0003F975
		public FourCC typeStatic
		{
			get
			{
				return QueryCanRunInBackground.Type;
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0004177C File Offset: 0x0003F97C
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
