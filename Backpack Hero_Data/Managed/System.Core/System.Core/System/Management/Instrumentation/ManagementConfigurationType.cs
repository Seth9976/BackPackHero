using System;

namespace System.Management.Instrumentation
{
	/// <summary>Represents the possible commit behaviors of a read/write property. It is used as the value of a parameter of the <see cref="T:System.Management.Instrumentation.ManagementConfigurationAttribute" /> attribute.</summary>
	// Token: 0x0200037E RID: 894
	public enum ManagementConfigurationType
	{
		/// <summary>Set values take effect only when Commit is called.</summary>
		// Token: 0x04000D05 RID: 3333
		Apply,
		/// <summary>Set values are applied immediately.</summary>
		// Token: 0x04000D06 RID: 3334
		OnCommit
	}
}
