using System;

namespace System.Configuration
{
	/// <summary>Specifies the default value for an application settings property.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001AD RID: 429
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DefaultSettingValueAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.DefaultSettingValueAttribute" /> class.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that represents the default value for the property. </param>
		// Token: 0x06000B71 RID: 2929 RVA: 0x0003111A File Offset: 0x0002F31A
		public DefaultSettingValueAttribute(string value)
		{
			this.value = value;
		}

		/// <summary>Gets the default value for the application settings property.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the default value for the property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00031129 File Offset: 0x0002F329
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400077E RID: 1918
		private string value;
	}
}
