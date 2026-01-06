using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200015E RID: 350
	public sealed class Recursion : Recursion<object>
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x000283F1 File Offset: 0x000265F1
		private Recursion()
		{
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x000283F9 File Offset: 0x000265F9
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x00028400 File Offset: 0x00026600
		public static int defaultMaxDepth { get; set; } = 100;

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00028408 File Offset: 0x00026608
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x0002840F File Offset: 0x0002660F
		public static bool safeMode { get; set; }

		// Token: 0x0600094F RID: 2383 RVA: 0x00028417 File Offset: 0x00026617
		internal static void OnRuntimeMethodLoad()
		{
			Recursion.safeMode = Application.isEditor || Debug.isDebugBuild;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0002842D File Offset: 0x0002662D
		protected override void Free()
		{
			GenericPool<Recursion>.Free(this);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00028435 File Offset: 0x00026635
		public new static Recursion New()
		{
			return Recursion.New(Recursion.defaultMaxDepth);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00028444 File Offset: 0x00026644
		public new static Recursion New(int maxDepth)
		{
			if (!Recursion.safeMode)
			{
				return null;
			}
			if (maxDepth < 1)
			{
				throw new ArgumentException("Max recursion depth must be at least one.", "maxDepth");
			}
			Recursion recursion = GenericPool<Recursion>.New(() => new Recursion());
			recursion.maxDepth = maxDepth;
			return recursion;
		}
	}
}
