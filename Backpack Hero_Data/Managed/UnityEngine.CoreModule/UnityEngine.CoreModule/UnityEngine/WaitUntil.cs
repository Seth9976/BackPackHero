using System;

namespace UnityEngine
{
	// Token: 0x0200022D RID: 557
	public sealed class WaitUntil : CustomYieldInstruction
	{
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00026CDC File Offset: 0x00024EDC
		public override bool keepWaiting
		{
			get
			{
				return !this.m_Predicate.Invoke();
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00026CFC File Offset: 0x00024EFC
		public WaitUntil(Func<bool> predicate)
		{
			this.m_Predicate = predicate;
		}

		// Token: 0x04000831 RID: 2097
		private Func<bool> m_Predicate;
	}
}
