using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C0 RID: 448
	[UsedByNativeCode]
	public struct GradientAlphaKey
	{
		// Token: 0x060013AB RID: 5035 RVA: 0x0001C41A File Offset: 0x0001A61A
		public GradientAlphaKey(float alpha, float time)
		{
			this.alpha = alpha;
			this.time = time;
		}

		// Token: 0x0400073F RID: 1855
		public float alpha;

		// Token: 0x04000740 RID: 1856
		public float time;
	}
}
