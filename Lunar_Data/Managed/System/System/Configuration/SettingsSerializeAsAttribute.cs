using System;

namespace System.Configuration
{
	/// <summary>Specifies the serialization mechanism that the settings provider should use. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001D8 RID: 472
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsSerializeAsAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsSerializeAsAttribute" /> class.</summary>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> enumerated value that specifies the serialization scheme.</param>
		// Token: 0x06000C55 RID: 3157 RVA: 0x000326B2 File Offset: 0x000308B2
		public SettingsSerializeAsAttribute(SettingsSerializeAs serializeAs)
		{
			this.serializeAs = serializeAs;
		}

		/// <summary>Gets the <see cref="T:System.Configuration.SettingsSerializeAs" /> enumeration value that specifies the serialization scheme.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> enumerated value that specifies the serialization scheme.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x000326C1 File Offset: 0x000308C1
		public SettingsSerializeAs SerializeAs
		{
			get
			{
				return this.serializeAs;
			}
		}

		// Token: 0x040007B7 RID: 1975
		private SettingsSerializeAs serializeAs;
	}
}
