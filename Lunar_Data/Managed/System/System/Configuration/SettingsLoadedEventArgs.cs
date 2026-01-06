using System;

namespace System.Configuration
{
	/// <summary>Provides data for the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
	// Token: 0x020001C8 RID: 456
	public class SettingsLoadedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> class. </summary>
		/// <param name="provider">A <see cref="T:System.Configuration.SettingsProvider" /> object from which settings are loaded.</param>
		// Token: 0x06000BF0 RID: 3056 RVA: 0x00031D9D File Offset: 0x0002FF9D
		public SettingsLoadedEventArgs(SettingsProvider provider)
		{
			this.provider = provider;
		}

		/// <summary>Gets the settings provider used to store configuration settings.</summary>
		/// <returns>A settings provider.</returns>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00031DAC File Offset: 0x0002FFAC
		public SettingsProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x04000798 RID: 1944
		private SettingsProvider provider;
	}
}
