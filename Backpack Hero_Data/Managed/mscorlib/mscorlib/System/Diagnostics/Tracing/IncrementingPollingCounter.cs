using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A00 RID: 2560
	public class IncrementingPollingCounter : DiagnosticCounter
	{
		// Token: 0x06005B4A RID: 23370 RVA: 0x00134430 File Offset: 0x00132630
		public IncrementingPollingCounter(string name, EventSource eventSource, Func<double> totalValueProvider)
			: base(name, eventSource)
		{
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06005B4B RID: 23371 RVA: 0x00134900 File Offset: 0x00132B00
		// (set) Token: 0x06005B4C RID: 23372 RVA: 0x00134908 File Offset: 0x00132B08
		public TimeSpan DisplayRateTimeScale { get; set; }
	}
}
