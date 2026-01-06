using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Provides contextual information that the provider can use when persisting settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C4 RID: 452
	[Serializable]
	public class SettingsContext : Hashtable
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00031D47 File Offset: 0x0002FF47
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x00031D4F File Offset: 0x0002FF4F
		internal ApplicationSettingsBase CurrentSettings
		{
			get
			{
				return this.current;
			}
			set
			{
				this.current = value;
			}
		}

		// Token: 0x04000794 RID: 1940
		[NonSerialized]
		private ApplicationSettingsBase current;
	}
}
