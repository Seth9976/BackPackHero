using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x0200004A RID: 74
	[UsedByNativeCode]
	[DebuggerDisplay("Value = {Value}; Count = {Count}")]
	public struct ProfilerRecorderSample
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002A34 File Offset: 0x00000C34
		public long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002A3C File Offset: 0x00000C3C
		public long Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x0400012B RID: 299
		private long value;

		// Token: 0x0400012C RID: 300
		private long count;

		// Token: 0x0400012D RID: 301
		private long refValue;
	}
}
