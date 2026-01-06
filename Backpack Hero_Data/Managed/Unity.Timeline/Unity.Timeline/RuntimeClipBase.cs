using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000021 RID: 33
	internal abstract class RuntimeClipBase : RuntimeElement
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600020B RID: 523
		public abstract double start { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600020C RID: 524
		public abstract double duration { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00007FFC File Offset: 0x000061FC
		public override long intervalStart
		{
			get
			{
				return DiscreteTime.GetNearestTick(this.start);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00008009 File Offset: 0x00006209
		public override long intervalEnd
		{
			get
			{
				return DiscreteTime.GetNearestTick(this.start + this.duration);
			}
		}
	}
}
