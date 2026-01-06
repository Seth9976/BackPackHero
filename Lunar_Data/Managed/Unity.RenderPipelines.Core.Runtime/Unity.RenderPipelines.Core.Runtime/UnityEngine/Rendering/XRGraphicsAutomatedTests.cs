using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E7 RID: 231
	public static class XRGraphicsAutomatedTests
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001EC08 File Offset: 0x0001CE08
		private static bool activatedFromCommandLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001EC0B File Offset: 0x0001CE0B
		public static bool enabled { get; } = XRGraphicsAutomatedTests.activatedFromCommandLine;

		// Token: 0x040003C6 RID: 966
		public static bool running = false;
	}
}
