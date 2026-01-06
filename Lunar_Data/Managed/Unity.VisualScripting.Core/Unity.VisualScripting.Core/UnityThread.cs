using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace Unity.VisualScripting
{
	// Token: 0x0200014C RID: 332
	public static class UnityThread
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00026E27 File Offset: 0x00025027
		public static bool allowsAPI
		{
			get
			{
				return !Serialization.isUnitySerializing && Thread.CurrentThread == UnityThread.thread;
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00026E3E File Offset: 0x0002503E
		internal static void RuntimeInitialize()
		{
			UnityThread.thread = Thread.CurrentThread;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00026E4A File Offset: 0x0002504A
		[Conditional("UNITY_EDITOR")]
		public static void EditorAsync(Action action)
		{
			if (UnityThread.editorAsync == null)
			{
				UnityThread.pendingQueue.Enqueue(action);
				return;
			}
			UnityThread.editorAsync(action);
		}

		// Token: 0x04000223 RID: 547
		public static Thread thread = Thread.CurrentThread;

		// Token: 0x04000224 RID: 548
		public static Action<Action> editorAsync;

		// Token: 0x04000225 RID: 549
		public static ConcurrentQueue<Action> pendingQueue = new ConcurrentQueue<Action>();
	}
}
