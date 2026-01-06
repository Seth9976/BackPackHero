using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000BB RID: 187
	public static class PlatformUtility
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x0000A46E File Offset: 0x0000866E
		private static bool CheckJitSupport()
		{
			return false;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000A471 File Offset: 0x00008671
		public static bool IsEditor(this RuntimePlatform platform)
		{
			return platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.LinuxEditor;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000A481 File Offset: 0x00008681
		public static bool IsStandalone(this RuntimePlatform platform)
		{
			return platform == RuntimePlatform.WindowsPlayer || platform == RuntimePlatform.OSXPlayer || platform == RuntimePlatform.LinuxPlayer;
		}

		// Token: 0x04000101 RID: 257
		public static readonly bool supportsJit = PlatformUtility.CheckJitSupport();
	}
}
