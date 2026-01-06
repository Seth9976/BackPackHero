using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000010 RID: 16
	internal class Metrics : IMetrics
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002D40 File Offset: 0x00000F40
		internal MetricsHandler Handler { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002D48 File Offset: 0x00000F48
		internal IDictionary<string, string> PackageTags { get; }

		// Token: 0x06000048 RID: 72 RVA: 0x00002D50 File Offset: 0x00000F50
		public Metrics(MetricsHandler handler, IDictionary<string, string> packageTags)
		{
			this.Handler = handler;
			this.PackageTags = packageTags;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D68 File Offset: 0x00000F68
		internal Metric CreateMetric(string name, double value, MetricType type, IDictionary<string, string> tags)
		{
			return new Metric
			{
				Name = name,
				Value = value,
				Type = type,
				Tags = ((tags == null) ? this.PackageTags : tags.MergeAllowOverride(this.PackageTags))
			};
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002DB8 File Offset: 0x00000FB8
		void IMetrics.SendGaugeMetric(string name, double value, IDictionary<string, string> tags)
		{
			Metric metric = this.CreateMetric(name, value, MetricType.Gauge, tags);
			this.Handler.Register(metric);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DDC File Offset: 0x00000FDC
		void IMetrics.SendHistogramMetric(string name, double time, IDictionary<string, string> tags)
		{
			Metric metric = this.CreateMetric(name, time, MetricType.Histogram, tags);
			this.Handler.Register(metric);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E00 File Offset: 0x00001000
		void IMetrics.SendSumMetric(string name, double value, IDictionary<string, string> tags)
		{
			Metric metric = this.CreateMetric(name, value, MetricType.Sum, tags);
			this.Handler.Register(metric);
		}
	}
}
