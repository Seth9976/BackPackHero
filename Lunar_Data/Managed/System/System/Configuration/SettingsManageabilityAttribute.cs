using System;

namespace System.Configuration
{
	/// <summary>Specifies special services for application settings properties. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001CB RID: 459
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsManageabilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsManageabilityAttribute" /> class.</summary>
		/// <param name="manageability">A <see cref="T:System.Configuration.SettingsManageability" /> value that enumerates the services being requested. </param>
		// Token: 0x06000BF6 RID: 3062 RVA: 0x00031DB4 File Offset: 0x0002FFB4
		public SettingsManageabilityAttribute(SettingsManageability manageability)
		{
			this.manageability = manageability;
		}

		/// <summary>Gets the set of special services that have been requested.</summary>
		/// <returns>A value that results from using the logical OR operator to combine all the <see cref="T:System.Configuration.SettingsManageability" /> enumeration values corresponding to the requested services.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00031DC3 File Offset: 0x0002FFC3
		public SettingsManageability Manageability
		{
			get
			{
				return this.manageability;
			}
		}

		// Token: 0x0400079B RID: 1947
		private SettingsManageability manageability;
	}
}
