using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000184 RID: 388
	public static class Packsize
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x0000D448 File Offset: 0x0000B648
		public static bool Test()
		{
			int num = Marshal.SizeOf(typeof(Packsize.ValvePackingSentinel_t));
			int num2 = Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t));
			return num == 32 && num2 == 616;
		}

		// Token: 0x04000A0A RID: 2570
		public const int value = 8;

		// Token: 0x020001E4 RID: 484
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		private struct ValvePackingSentinel_t
		{
			// Token: 0x04000ACE RID: 2766
			private uint m_u32;

			// Token: 0x04000ACF RID: 2767
			private ulong m_u64;

			// Token: 0x04000AD0 RID: 2768
			private ushort m_u16;

			// Token: 0x04000AD1 RID: 2769
			private double m_d;
		}
	}
}
