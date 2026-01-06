using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000211 RID: 529
	internal class AssertSection : ConfigurationElement
	{
		// Token: 0x06000F41 RID: 3905 RVA: 0x0004499C File Offset: 0x00042B9C
		static AssertSection()
		{
			AssertSection._properties.Add(AssertSection._propAssertUIEnabled);
			AssertSection._properties.Add(AssertSection._propLogFile);
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x00044A10 File Offset: 0x00042C10
		[ConfigurationProperty("assertuienabled", DefaultValue = true)]
		public bool AssertUIEnabled
		{
			get
			{
				return (bool)base[AssertSection._propAssertUIEnabled];
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00044A22 File Offset: 0x00042C22
		[ConfigurationProperty("logfilename", DefaultValue = "")]
		public string LogFileName
		{
			get
			{
				return (string)base[AssertSection._propLogFile];
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00044A34 File Offset: 0x00042C34
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssertSection._properties;
			}
		}

		// Token: 0x0400098B RID: 2443
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x0400098C RID: 2444
		private static readonly ConfigurationProperty _propAssertUIEnabled = new ConfigurationProperty("assertuienabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x0400098D RID: 2445
		private static readonly ConfigurationProperty _propLogFile = new ConfigurationProperty("logfilename", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
