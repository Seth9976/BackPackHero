using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x0200000E RID: 14
	internal class DisabledMetrics : IMetrics
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002D10 File Offset: 0x00000F10
		void IMetrics.SendGaugeMetric(string name, double value, IDictionary<string, string> tags)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D12 File Offset: 0x00000F12
		void IMetrics.SendHistogramMetric(string name, double time, IDictionary<string, string> tags)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D14 File Offset: 0x00000F14
		void IMetrics.SendSumMetric(string name, double value, IDictionary<string, string> tags)
		{
		}
	}
}
