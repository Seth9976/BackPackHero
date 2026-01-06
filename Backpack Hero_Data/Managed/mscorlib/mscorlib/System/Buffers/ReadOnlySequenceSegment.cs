using System;

namespace System.Buffers
{
	// Token: 0x02000AEC RID: 2796
	public abstract class ReadOnlySequenceSegment<T>
	{
		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06006366 RID: 25446 RVA: 0x0014C9F0 File Offset: 0x0014ABF0
		// (set) Token: 0x06006367 RID: 25447 RVA: 0x0014C9F8 File Offset: 0x0014ABF8
		public ReadOnlyMemory<T> Memory { get; protected set; }

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06006368 RID: 25448 RVA: 0x0014CA01 File Offset: 0x0014AC01
		// (set) Token: 0x06006369 RID: 25449 RVA: 0x0014CA09 File Offset: 0x0014AC09
		public ReadOnlySequenceSegment<T> Next { get; protected set; }

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x0600636A RID: 25450 RVA: 0x0014CA12 File Offset: 0x0014AC12
		// (set) Token: 0x0600636B RID: 25451 RVA: 0x0014CA1A File Offset: 0x0014AC1A
		public long RunningIndex { get; protected set; }
	}
}
