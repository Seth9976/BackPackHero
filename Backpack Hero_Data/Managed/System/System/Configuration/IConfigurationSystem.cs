using System;
using System.Runtime.InteropServices;

namespace System.Configuration
{
	/// <summary>Provides standard configuration methods.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B2 RID: 434
	[ComVisible(false)]
	public interface IConfigurationSystem
	{
		/// <summary>Gets the specified configuration.</summary>
		/// <returns>The object representing the configuration.</returns>
		/// <param name="configKey">The configuration key.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B7D RID: 2941
		object GetConfig(string configKey);

		/// <summary>Used for initialization.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B7E RID: 2942
		void Init();
	}
}
