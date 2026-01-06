using System;

namespace System.Configuration
{
	/// <summary>Specifies the settings provider used to provide storage for the current application settings class or property. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001D4 RID: 468
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsProviderAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsProviderAttribute" /> class.</summary>
		/// <param name="providerTypeName">A <see cref="T:System.String" /> containing the name of the settings provider.</param>
		// Token: 0x06000C4B RID: 3147 RVA: 0x0003261B File Offset: 0x0003081B
		public SettingsProviderAttribute(string providerTypeName)
		{
			if (providerTypeName == null)
			{
				throw new ArgumentNullException("providerTypeName");
			}
			this.providerTypeName = providerTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProviderAttribute" /> class. </summary>
		/// <param name="providerType">A <see cref="T:System.Type" /> containing the settings provider type.</param>
		// Token: 0x06000C4C RID: 3148 RVA: 0x00032638 File Offset: 0x00030838
		public SettingsProviderAttribute(Type providerType)
		{
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			this.providerTypeName = providerType.AssemblyQualifiedName;
		}

		/// <summary>Gets the type name of the settings provider.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the settings provider.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00032660 File Offset: 0x00030860
		public string ProviderTypeName
		{
			get
			{
				return this.providerTypeName;
			}
		}

		// Token: 0x040007B1 RID: 1969
		private string providerTypeName;
	}
}
