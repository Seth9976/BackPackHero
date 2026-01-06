using System;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x0200008F RID: 143
	public static class NativeLeakDetection
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00004399 File Offset: 0x00002599
		[RuntimeInitializeOnLoadMethod]
		private static void Initialize()
		{
			NativeLeakDetection.s_NativeLeakDetectionMode = 1;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000043A4 File Offset: 0x000025A4
		// (set) Token: 0x06000256 RID: 598 RVA: 0x000043D0 File Offset: 0x000025D0
		public static NativeLeakDetectionMode Mode
		{
			get
			{
				bool flag = NativeLeakDetection.s_NativeLeakDetectionMode == 0;
				if (flag)
				{
					NativeLeakDetection.Initialize();
				}
				return (NativeLeakDetectionMode)NativeLeakDetection.s_NativeLeakDetectionMode;
			}
			set
			{
				bool flag = NativeLeakDetection.s_NativeLeakDetectionMode != (int)value;
				if (flag)
				{
					NativeLeakDetection.s_NativeLeakDetectionMode = (int)value;
				}
			}
		}

		// Token: 0x0400021F RID: 543
		private static int s_NativeLeakDetectionMode;

		// Token: 0x04000220 RID: 544
		private const string kNativeLeakDetectionModePrefsString = "Unity.Colletions.NativeLeakDetection.Mode";
	}
}
