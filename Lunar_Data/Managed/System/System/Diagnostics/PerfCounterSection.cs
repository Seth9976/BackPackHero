using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200021D RID: 541
	internal class PerfCounterSection : ConfigurationElement
	{
		// Token: 0x06000FBE RID: 4030 RVA: 0x00045FC0 File Offset: 0x000441C0
		static PerfCounterSection()
		{
			PerfCounterSection._properties.Add(PerfCounterSection._propFileMappingSize);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00045FFF File Offset: 0x000441FF
		[ConfigurationProperty("filemappingsize", DefaultValue = 524288)]
		public int FileMappingSize
		{
			get
			{
				return (int)base[PerfCounterSection._propFileMappingSize];
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00046011 File Offset: 0x00044211
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PerfCounterSection._properties;
			}
		}

		// Token: 0x040009A0 RID: 2464
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x040009A1 RID: 2465
		private static readonly ConfigurationProperty _propFileMappingSize = new ConfigurationProperty("filemappingsize", typeof(int), 524288, ConfigurationPropertyOptions.None);
	}
}
