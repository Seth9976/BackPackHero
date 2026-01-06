using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Represents a collection of key/value pairs used to describe a configuration object as well as a <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public class SettingsAttributeDictionary : Hashtable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsAttributeDictionary" /> class. </summary>
		// Token: 0x06000BD7 RID: 3031 RVA: 0x00031995 File Offset: 0x0002FB95
		public SettingsAttributeDictionary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsAttributeDictionary" /> class. </summary>
		/// <param name="attributes">A collection of key/value pairs that are related to configuration settings.</param>
		// Token: 0x06000BD8 RID: 3032 RVA: 0x0003199D File Offset: 0x0002FB9D
		public SettingsAttributeDictionary(SettingsAttributeDictionary attributes)
			: base(attributes)
		{
		}
	}
}
