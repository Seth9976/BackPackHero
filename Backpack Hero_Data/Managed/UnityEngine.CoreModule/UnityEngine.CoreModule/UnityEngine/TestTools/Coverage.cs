using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.TestTools
{
	// Token: 0x0200048D RID: 1165
	[NativeClass("ScriptingCoverage")]
	[NativeType("Runtime/Scripting/ScriptingCoverage.h")]
	public static class Coverage
	{
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002931 RID: 10545
		// (set) Token: 0x06002932 RID: 10546
		public static extern bool enabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06002933 RID: 10547
		[FreeFunction("ScriptingCoverageGetCoverageForMethodInfoObject", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern CoveredSequencePoint[] GetSequencePointsFor_Internal(MethodBase method);

		// Token: 0x06002934 RID: 10548
		[FreeFunction("ScriptingCoverageResetForMethodInfoObject", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void ResetFor_Internal(MethodBase method);

		// Token: 0x06002935 RID: 10549 RVA: 0x00044238 File Offset: 0x00042438
		[FreeFunction("ScriptingCoverageGetStatsForMethodInfoObject", ThrowsException = true)]
		private static CoveredMethodStats GetStatsFor_Internal(MethodBase method)
		{
			CoveredMethodStats coveredMethodStats;
			Coverage.GetStatsFor_Internal_Injected(method, out coveredMethodStats);
			return coveredMethodStats;
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x00044250 File Offset: 0x00042450
		public static CoveredSequencePoint[] GetSequencePointsFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			return Coverage.GetSequencePointsFor_Internal(method);
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x0004427C File Offset: 0x0004247C
		public static CoveredMethodStats GetStatsFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			return Coverage.GetStatsFor_Internal(method);
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000442A8 File Offset: 0x000424A8
		public static CoveredMethodStats[] GetStatsFor(MethodBase[] methods)
		{
			bool flag = methods == null;
			if (flag)
			{
				throw new ArgumentNullException("methods");
			}
			CoveredMethodStats[] array = new CoveredMethodStats[methods.Length];
			for (int i = 0; i < methods.Length; i++)
			{
				array[i] = Coverage.GetStatsFor(methods[i]);
			}
			return array;
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000442FC File Offset: 0x000424FC
		public static CoveredMethodStats[] GetStatsFor(Type type)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new ArgumentNullException("type");
			}
			return Coverage.GetStatsFor(Enumerable.ToArray<MethodBase>(Enumerable.OfType<MethodBase>(type.GetMembers(62))));
		}

		// Token: 0x0600293A RID: 10554
		[FreeFunction("ScriptingCoverageGetStatsForAllCoveredMethodsFromScripting", ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern CoveredMethodStats[] GetStatsForAllCoveredMethods();

		// Token: 0x0600293B RID: 10555 RVA: 0x00044338 File Offset: 0x00042538
		public static void ResetFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			Coverage.ResetFor_Internal(method);
		}

		// Token: 0x0600293C RID: 10556
		[FreeFunction("ScriptingCoverageResetAllFromScripting", ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern void ResetAll();

		// Token: 0x0600293D RID: 10557
		[MethodImpl(4096)]
		private static extern void GetStatsFor_Internal_Injected(MethodBase method, out CoveredMethodStats ret);
	}
}
