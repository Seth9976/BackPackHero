using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000022 RID: 34
	internal abstract class RuntimeElement : IInterval
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000210 RID: 528
		public abstract long intervalStart { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000211 RID: 529
		public abstract long intervalEnd { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00008025 File Offset: 0x00006225
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000802D File Offset: 0x0000622D
		public int intervalBit { get; set; }

		// Token: 0x1700009E RID: 158
		// (set) Token: 0x06000214 RID: 532
		public abstract bool enable { set; }

		// Token: 0x06000215 RID: 533
		public abstract void EvaluateAt(double localTime, FrameData frameData);

		// Token: 0x06000216 RID: 534
		public abstract void DisableAt(double localTime, double rootDuration, FrameData frameData);
	}
}
