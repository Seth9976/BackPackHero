using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000174 RID: 372
	internal static class Platform
	{
		// Token: 0x060009DB RID: 2523
		[DllImport("libc")]
		private static extern int uname(IntPtr buf);

		// Token: 0x060009DC RID: 2524 RVA: 0x0002B8F8 File Offset: 0x00029AF8
		private static void CheckOS()
		{
			if (Environment.OSVersion.Platform != PlatformID.Unix)
			{
				Platform.checkedOS = true;
				return;
			}
			IntPtr intPtr = Marshal.AllocHGlobal(8192);
			if (Platform.uname(intPtr) == 0)
			{
				string text = Marshal.PtrToStringAnsi(intPtr);
				if (!(text == "Darwin"))
				{
					if (!(text == "FreeBSD"))
					{
						if (!(text == "AIX"))
						{
							if (!(text == "OS400"))
							{
								if (text == "OpenBSD")
								{
									Platform.isOpenBSD = true;
								}
							}
							else
							{
								Platform.isIBMi = true;
							}
						}
						else
						{
							Platform.isAix = true;
						}
					}
					else
					{
						Platform.isFreeBSD = true;
					}
				}
				else
				{
					Platform.isMacOS = true;
				}
			}
			Marshal.FreeHGlobal(intPtr);
			Platform.checkedOS = true;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0002B9A8 File Offset: 0x00029BA8
		public static bool IsMacOS
		{
			get
			{
				if (!Platform.checkedOS)
				{
					try
					{
						Platform.CheckOS();
					}
					catch (DllNotFoundException)
					{
						Platform.isMacOS = false;
					}
				}
				return Platform.isMacOS;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0002B9E4 File Offset: 0x00029BE4
		public static bool IsFreeBSD
		{
			get
			{
				if (!Platform.checkedOS)
				{
					Platform.CheckOS();
				}
				return Platform.isFreeBSD;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0002B9F7 File Offset: 0x00029BF7
		public static bool IsOpenBSD
		{
			get
			{
				if (!Platform.checkedOS)
				{
					Platform.CheckOS();
				}
				return Platform.isOpenBSD;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0002BA0A File Offset: 0x00029C0A
		public static bool IsIBMi
		{
			get
			{
				if (!Platform.checkedOS)
				{
					Platform.CheckOS();
				}
				return Platform.isIBMi;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0002BA1D File Offset: 0x00029C1D
		public static bool IsAix
		{
			get
			{
				if (!Platform.checkedOS)
				{
					Platform.CheckOS();
				}
				return Platform.isAix;
			}
		}

		// Token: 0x040006AF RID: 1711
		private static bool checkedOS;

		// Token: 0x040006B0 RID: 1712
		private static bool isMacOS;

		// Token: 0x040006B1 RID: 1713
		private static bool isAix;

		// Token: 0x040006B2 RID: 1714
		private static bool isIBMi;

		// Token: 0x040006B3 RID: 1715
		private static bool isFreeBSD;

		// Token: 0x040006B4 RID: 1716
		private static bool isOpenBSD;
	}
}
