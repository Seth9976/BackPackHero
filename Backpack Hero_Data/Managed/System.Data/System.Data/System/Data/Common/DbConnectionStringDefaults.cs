using System;
using System.Data.SqlClient;

namespace System.Data.Common
{
	// Token: 0x0200037B RID: 891
	internal static class DbConnectionStringDefaults
	{
		// Token: 0x040019F7 RID: 6647
		internal const ApplicationIntent ApplicationIntent = ApplicationIntent.ReadWrite;

		// Token: 0x040019F8 RID: 6648
		internal const string ApplicationName = "Core .Net SqlClient Data Provider";

		// Token: 0x040019F9 RID: 6649
		internal const string AttachDBFilename = "";

		// Token: 0x040019FA RID: 6650
		internal const int ConnectTimeout = 15;

		// Token: 0x040019FB RID: 6651
		internal const string CurrentLanguage = "";

		// Token: 0x040019FC RID: 6652
		internal const string DataSource = "";

		// Token: 0x040019FD RID: 6653
		internal const bool Encrypt = false;

		// Token: 0x040019FE RID: 6654
		internal const bool Enlist = true;

		// Token: 0x040019FF RID: 6655
		internal const string FailoverPartner = "";

		// Token: 0x04001A00 RID: 6656
		internal const string InitialCatalog = "";

		// Token: 0x04001A01 RID: 6657
		internal const bool IntegratedSecurity = false;

		// Token: 0x04001A02 RID: 6658
		internal const int LoadBalanceTimeout = 0;

		// Token: 0x04001A03 RID: 6659
		internal const bool MultipleActiveResultSets = false;

		// Token: 0x04001A04 RID: 6660
		internal const bool MultiSubnetFailover = false;

		// Token: 0x04001A05 RID: 6661
		internal const int MaxPoolSize = 100;

		// Token: 0x04001A06 RID: 6662
		internal const int MinPoolSize = 0;

		// Token: 0x04001A07 RID: 6663
		internal const int PacketSize = 8000;

		// Token: 0x04001A08 RID: 6664
		internal const string Password = "";

		// Token: 0x04001A09 RID: 6665
		internal const bool PersistSecurityInfo = false;

		// Token: 0x04001A0A RID: 6666
		internal const bool Pooling = true;

		// Token: 0x04001A0B RID: 6667
		internal const bool TrustServerCertificate = false;

		// Token: 0x04001A0C RID: 6668
		internal const string TypeSystemVersion = "Latest";

		// Token: 0x04001A0D RID: 6669
		internal const string UserID = "";

		// Token: 0x04001A0E RID: 6670
		internal const bool UserInstance = false;

		// Token: 0x04001A0F RID: 6671
		internal const bool Replication = false;

		// Token: 0x04001A10 RID: 6672
		internal const string WorkstationID = "";

		// Token: 0x04001A11 RID: 6673
		internal const string TransactionBinding = "Implicit Unbind";

		// Token: 0x04001A12 RID: 6674
		internal const int ConnectRetryCount = 1;

		// Token: 0x04001A13 RID: 6675
		internal const int ConnectRetryInterval = 10;

		// Token: 0x04001A14 RID: 6676
		internal const string Dsn = "";

		// Token: 0x04001A15 RID: 6677
		internal const string Driver = "";
	}
}
