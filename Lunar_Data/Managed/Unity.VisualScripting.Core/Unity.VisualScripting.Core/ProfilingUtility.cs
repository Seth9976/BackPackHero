using System;
using System.Diagnostics;
using System.Threading;

namespace Unity.VisualScripting
{
	// Token: 0x020000C9 RID: 201
	public static class ProfilingUtility
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000AD13 File Offset: 0x00008F13
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0000AD1A File Offset: 0x00008F1A
		public static ProfiledSegment rootSegment { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000AD22 File Offset: 0x00008F22
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0000AD29 File Offset: 0x00008F29
		public static ProfiledSegment currentSegment { get; set; } = (ProfilingUtility.rootSegment = new ProfiledSegment(null, "Root"));

		// Token: 0x060004DC RID: 1244 RVA: 0x0000AD31 File Offset: 0x00008F31
		[Conditional("ENABLE_PROFILER")]
		public static void Clear()
		{
			ProfilingUtility.currentSegment = (ProfilingUtility.rootSegment = new ProfiledSegment(null, "Root"));
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000AD49 File Offset: 0x00008F49
		public static ProfilingScope SampleBlock(string name)
		{
			return new ProfilingScope(name);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000AD54 File Offset: 0x00008F54
		[Conditional("ENABLE_PROFILER")]
		public static void BeginSample(string name)
		{
			Monitor.Enter(ProfilingUtility.@lock);
			if (!ProfilingUtility.currentSegment.children.Contains(name))
			{
				ProfilingUtility.currentSegment.children.Add(new ProfiledSegment(ProfilingUtility.currentSegment, name));
			}
			ProfilingUtility.currentSegment = ProfilingUtility.currentSegment.children[name];
			ProfiledSegment currentSegment = ProfilingUtility.currentSegment;
			long calls = currentSegment.calls;
			currentSegment.calls = calls + 1L;
			ProfilingUtility.currentSegment.stopwatch.Start();
			bool allowsAPI = UnityThread.allowsAPI;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000ADD6 File Offset: 0x00008FD6
		[Conditional("ENABLE_PROFILER")]
		public static void EndSample()
		{
			ProfilingUtility.currentSegment.stopwatch.Stop();
			if (ProfilingUtility.currentSegment.parent != null)
			{
				ProfilingUtility.currentSegment = ProfilingUtility.currentSegment.parent;
			}
			bool allowsAPI = UnityThread.allowsAPI;
			Monitor.Exit(ProfilingUtility.@lock);
		}

		// Token: 0x04000119 RID: 281
		private static readonly object @lock = new object();
	}
}
