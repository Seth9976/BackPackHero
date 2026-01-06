using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Provides data for the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BD RID: 445
	public class SettingChangingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingChangingEventArgs" /> class.</summary>
		/// <param name="settingName">A <see cref="T:System.String" /> containing the name of the application setting.</param>
		/// <param name="settingClass">A <see cref="T:System.String" /> containing a category description of the setting. Often this parameter is set to the application settings group name.</param>
		/// <param name="settingKey">A <see cref="T:System.String" /> containing the application settings key.</param>
		/// <param name="newValue">An <see cref="T:System.Object" /> that contains the new value to be assigned to the application settings property.</param>
		/// <param name="cancel">true to cancel the event; otherwise, false. </param>
		// Token: 0x06000BA8 RID: 2984 RVA: 0x0003142A File Offset: 0x0002F62A
		public SettingChangingEventArgs(string settingName, string settingClass, string settingKey, object newValue, bool cancel)
			: base(cancel)
		{
			this.settingName = settingName;
			this.settingClass = settingClass;
			this.settingKey = settingKey;
			this.newValue = newValue;
		}

		/// <summary>Gets the name of the application setting associated with the application settings property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the application setting. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00031451 File Offset: 0x0002F651
		public string SettingName
		{
			get
			{
				return this.settingName;
			}
		}

		/// <summary>Gets the application settings property category.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a category description of the setting. Typically, this parameter is set to the application settings group name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00031459 File Offset: 0x0002F659
		public string SettingClass
		{
			get
			{
				return this.settingClass;
			}
		}

		/// <summary>Gets the application settings key associated with the property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the application settings key.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00031461 File Offset: 0x0002F661
		public string SettingKey
		{
			get
			{
				return this.settingKey;
			}
		}

		/// <summary>Gets the new value being assigned to the application settings property.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the new value to be assigned to the application settings property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00031469 File Offset: 0x0002F669
		public object NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		// Token: 0x04000785 RID: 1925
		private string settingName;

		// Token: 0x04000786 RID: 1926
		private string settingClass;

		// Token: 0x04000787 RID: 1927
		private string settingKey;

		// Token: 0x04000788 RID: 1928
		private object newValue;
	}
}
