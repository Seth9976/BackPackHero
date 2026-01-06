using System;

namespace System.Configuration
{
	/// <summary>Indicates that an application settings property has a special significance. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001DB RID: 475
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SpecialSettingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SpecialSettingAttribute" /> class.</summary>
		/// <param name="specialSetting">A <see cref="T:System.Configuration.SpecialSetting" /> enumeration value defining the category of the application settings property.</param>
		// Token: 0x06000C59 RID: 3161 RVA: 0x00032735 File Offset: 0x00030935
		public SpecialSettingAttribute(SpecialSetting specialSetting)
		{
			this.setting = specialSetting;
		}

		/// <summary>Gets the value describing the special setting category of the application settings property.</summary>
		/// <returns>A <see cref="T:System.Configuration.SpecialSetting" /> enumeration value defining the category of the application settings property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00032744 File Offset: 0x00030944
		public SpecialSetting SpecialSetting
		{
			get
			{
				return this.setting;
			}
		}

		// Token: 0x040007BB RID: 1979
		private SpecialSetting setting;
	}
}
