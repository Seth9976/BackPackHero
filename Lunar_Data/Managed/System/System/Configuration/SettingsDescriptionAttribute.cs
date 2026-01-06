using System;

namespace System.Configuration
{
	/// <summary>Provides a string that describes an individual configuration property. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C5 RID: 453
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsDescriptionAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsDescriptionAttribute" /> class.</summary>
		/// <param name="description">The <see cref="T:System.String" /> used as descriptive text.</param>
		// Token: 0x06000BEA RID: 3050 RVA: 0x00031D58 File Offset: 0x0002FF58
		public SettingsDescriptionAttribute(string description)
		{
			this.desc = description;
		}

		/// <summary>Gets the descriptive text for the associated configuration property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the descriptive text for the associated configuration property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00031D67 File Offset: 0x0002FF67
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		// Token: 0x04000795 RID: 1941
		private string desc;
	}
}
