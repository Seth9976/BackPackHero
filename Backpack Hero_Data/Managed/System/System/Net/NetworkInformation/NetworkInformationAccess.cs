using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies permission to access information about network interfaces and traffic statistics.</summary>
	// Token: 0x02000506 RID: 1286
	[Flags]
	public enum NetworkInformationAccess
	{
		/// <summary>No access to network information.</summary>
		// Token: 0x04001861 RID: 6241
		None = 0,
		/// <summary>Read access to network information.</summary>
		// Token: 0x04001862 RID: 6242
		Read = 1,
		/// <summary>Ping access to network information.</summary>
		// Token: 0x04001863 RID: 6243
		Ping = 4
	}
}
