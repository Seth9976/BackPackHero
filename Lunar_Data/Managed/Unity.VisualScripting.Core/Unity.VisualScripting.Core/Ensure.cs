using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000049 RID: 73
	public static class Ensure
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00004F49 File Offset: 0x00003149
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00004F50 File Offset: 0x00003150
		public static bool IsActive { get; set; }

		// Token: 0x060001F1 RID: 497 RVA: 0x00004F58 File Offset: 0x00003158
		public static void Off()
		{
			Ensure.IsActive = false;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00004F60 File Offset: 0x00003160
		public static void On()
		{
			Ensure.IsActive = true;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00004F68 File Offset: 0x00003168
		public static EnsureThat That(string paramName)
		{
			Ensure.instance.paramName = paramName;
			return Ensure.instance;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00004F7A File Offset: 0x0000317A
		internal static void OnRuntimeMethodLoad()
		{
			Ensure.IsActive = Application.isEditor || Debug.isDebugBuild;
		}

		// Token: 0x0400004A RID: 74
		private static readonly EnsureThat instance = new EnsureThat();
	}
}
