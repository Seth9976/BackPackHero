using System;

namespace UnityEngine
{
	// Token: 0x0200022E RID: 558
	public sealed class WaitWhile : CustomYieldInstruction
	{
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x00026D10 File Offset: 0x00024F10
		public override bool keepWaiting
		{
			get
			{
				return this.m_Predicate.Invoke();
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00026D2D File Offset: 0x00024F2D
		public WaitWhile(Func<bool> predicate)
		{
			this.m_Predicate = predicate;
		}

		// Token: 0x04000832 RID: 2098
		private Func<bool> m_Predicate;
	}
}
