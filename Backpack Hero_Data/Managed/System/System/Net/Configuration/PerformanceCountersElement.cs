using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the performance counter element in the System.Net configuration file that determines whether networking performance counters are enabled. This class cannot be inherited.</summary>
	// Token: 0x0200057E RID: 1406
	public sealed class PerformanceCountersElement : ConfigurationElement
	{
		// Token: 0x06002C71 RID: 11377 RVA: 0x0009EB2D File Offset: 0x0009CD2D
		static PerformanceCountersElement()
		{
			PerformanceCountersElement.properties.Add(PerformanceCountersElement.enabledProp);
		}

		/// <summary>Gets or sets whether performance counters are enabled.</summary>
		/// <returns>true if performance counters are enabled; otherwise, false.</returns>
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x0009EB67 File Offset: 0x0009CD67
		// (set) Token: 0x06002C73 RID: 11379 RVA: 0x0009EB79 File Offset: 0x0009CD79
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[PerformanceCountersElement.enabledProp];
			}
			set
			{
				base[PerformanceCountersElement.enabledProp] = value;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x0009EB8C File Offset: 0x0009CD8C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PerformanceCountersElement.properties;
			}
		}

		// Token: 0x04001A60 RID: 6752
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x04001A61 RID: 6753
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
