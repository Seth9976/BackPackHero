using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200020D RID: 525
	internal struct FederatedAuthenticationFeatureExtensionData
	{
		// Token: 0x0400119A RID: 4506
		internal TdsEnums.FedAuthLibrary libraryType;

		// Token: 0x0400119B RID: 4507
		internal bool fedAuthRequiredPreLoginResponse;

		// Token: 0x0400119C RID: 4508
		internal byte[] accessToken;
	}
}
