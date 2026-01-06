using System;
using System.Collections.Generic;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Telemetry.Internal;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000047 RID: 71
	internal class CoreMetrics
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003936 File Offset: 0x00001B36
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000393D File Offset: 0x00001B3D
		public static CoreMetrics Instance { get; internal set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00003945 File Offset: 0x00001B45
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000394D File Offset: 0x00001B4D
		internal IMetrics Metrics { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00003956 File Offset: 0x00001B56
		internal IDictionary<Type, IMetrics> AllPackageMetrics { get; } = new Dictionary<Type, IMetrics>();

		// Token: 0x06000144 RID: 324 RVA: 0x0000395E File Offset: 0x00001B5E
		public void SendAllPackagesInitSuccessMetric()
		{
			if (this.Metrics != null)
			{
				this.Metrics.SendSumMetric("all_packages_init_success", 1.0, null);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00003982 File Offset: 0x00001B82
		public void SendAllPackagesInitTimeMetric(double initTimeSeconds)
		{
			if (this.Metrics != null)
			{
				this.Metrics.SendHistogramMetric("all_packages_init_time", initTimeSeconds, null);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000039A0 File Offset: 0x00001BA0
		public void SendInitTimeMetricForPackage(Type packageType, double initTimeSeconds)
		{
			IMetrics metrics;
			if (this.AllPackageMetrics.TryGetValue(packageType, out metrics))
			{
				metrics.SendHistogramMetric("package_init_time", initTimeSeconds, null);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000039CC File Offset: 0x00001BCC
		public void Initialize(IProjectConfiguration configuration, IMetricsFactory factory, Type corePackageType)
		{
			this.AllPackageMetrics.Clear();
			this.FindAndCacheAllPackageMetrics(configuration, factory);
			IMetrics metrics;
			if (this.AllPackageMetrics.TryGetValue(corePackageType, out metrics))
			{
				this.Metrics = metrics;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00003A04 File Offset: 0x00001C04
		internal void FindAndCacheAllPackageMetrics(IProjectConfiguration configuration, IMetricsFactory factory)
		{
			string @string = configuration.GetString("com.unity.services.core.all-package-names", "");
			foreach (string text in ((@string != null) ? @string.Split(';', StringSplitOptions.None) : null) ?? Array.Empty<string>())
			{
				if (!string.IsNullOrEmpty(text))
				{
					string text2 = string.Format("{0}.initializer-assembly-qualified-names", text);
					string string2 = configuration.GetString(text2, "");
					if (!string.IsNullOrEmpty(string2))
					{
						string[] array2 = string2.Split(';', StringSplitOptions.None);
						for (int j = 0; j < array2.Length; j++)
						{
							Type type = Type.GetType(array2[j]);
							if (type != null)
							{
								IMetrics metrics = factory.Create(text);
								this.AllPackageMetrics[type] = metrics;
							}
						}
					}
				}
			}
		}

		// Token: 0x04000056 RID: 86
		internal const string PackageInitTimeMetricName = "package_init_time";

		// Token: 0x04000057 RID: 87
		internal const string AllPackagesInitSuccessMetricName = "all_packages_init_success";

		// Token: 0x04000058 RID: 88
		internal const string AllPackagesInitTimeMetricName = "all_packages_init_time";

		// Token: 0x04000059 RID: 89
		internal const string PackageInitializerNamesKeyFormat = "{0}.initializer-assembly-qualified-names";

		// Token: 0x0400005A RID: 90
		internal const char PackageInitializerNamesSeparator = ';';

		// Token: 0x0400005B RID: 91
		internal const string AllPackageNamesKey = "com.unity.services.core.all-package-names";

		// Token: 0x0400005C RID: 92
		internal const char AllPackageNamesSeparator = ';';
	}
}
