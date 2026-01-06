using System;

namespace UnityEngine
{
	// Token: 0x0200022C RID: 556
	public class WaitForSecondsRealtime : CustomYieldInstruction
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00026C45 File Offset: 0x00024E45
		// (set) Token: 0x060017E7 RID: 6119 RVA: 0x00026C4D File Offset: 0x00024E4D
		public float waitTime { get; set; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x00026C58 File Offset: 0x00024E58
		public override bool keepWaiting
		{
			get
			{
				bool flag = this.m_WaitUntilTime < 0f;
				if (flag)
				{
					this.m_WaitUntilTime = Time.realtimeSinceStartup + this.waitTime;
				}
				bool flag2 = Time.realtimeSinceStartup < this.m_WaitUntilTime;
				bool flag3 = !flag2;
				if (flag3)
				{
					this.Reset();
				}
				return flag2;
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00026CAF File Offset: 0x00024EAF
		public WaitForSecondsRealtime(float time)
		{
			this.waitTime = time;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00026CCC File Offset: 0x00024ECC
		public override void Reset()
		{
			this.m_WaitUntilTime = -1f;
		}

		// Token: 0x04000830 RID: 2096
		private float m_WaitUntilTime = -1f;
	}
}
