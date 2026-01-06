using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Core
{
	// Token: 0x0200000C RID: 12
	internal static class UnityThreadUtils
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002337 File Offset: 0x00000537
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000233E File Offset: 0x0000053E
		internal static TaskScheduler UnityThreadScheduler { get; private set; }

		// Token: 0x0600002F RID: 47 RVA: 0x00002346 File Offset: 0x00000546
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void CaptureUnityThreadInfo()
		{
			UnityThreadUtils.s_UnityThreadId = Thread.CurrentThread.ManagedThreadId;
			UnityThreadUtils.UnityThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002361 File Offset: 0x00000561
		public static bool IsRunningOnUnityThread
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId == UnityThreadUtils.s_UnityThreadId;
			}
		}

		// Token: 0x04000019 RID: 25
		private static int s_UnityThreadId;
	}
}
