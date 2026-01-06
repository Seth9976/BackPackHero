using System;

namespace System.Management.Instrumentation
{
	/// <summary>Defines values that specify the hosting model for the provider.</summary>
	// Token: 0x02000382 RID: 898
	public enum ManagementHostingModel
	{
		/// <summary>Activates the provider as a decoupled provider.</summary>
		// Token: 0x04000D08 RID: 3336
		Decoupled,
		/// <summary>Activates the provider in the provider host process that is running under the LocalService account.</summary>
		// Token: 0x04000D09 RID: 3337
		LocalService = 2,
		/// <summary>Activates the provider in the provider host process that is running under the LocalSystem account.</summary>
		// Token: 0x04000D0A RID: 3338
		LocalSystem,
		/// <summary>Activates the provider in the provider host process that is running under the NetworkService account.</summary>
		// Token: 0x04000D0B RID: 3339
		NetworkService = 1
	}
}
