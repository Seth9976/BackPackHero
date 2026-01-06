using System;
using Unity.Services.Core.Device.Internal;
using Unity.Services.Core.Internal;
using UnityEngine;

namespace Unity.Services.Core.Device
{
	// Token: 0x02000002 RID: 2
	internal class InstallationId : IInstallationId, IServiceComponent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public InstallationId()
		{
			this.UnityAdsIdentifierProvider = new UnityAdsIdentifier();
			this.UnityAnalyticsIdentifierProvider = new UnityAnalyticsIdentifier();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206E File Offset: 0x0000026E
		public string GetOrCreateIdentifier()
		{
			if (string.IsNullOrEmpty(this.Identifier))
			{
				this.CreateIdentifier();
			}
			return this.Identifier;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000208C File Offset: 0x0000028C
		public void CreateIdentifier()
		{
			this.Identifier = InstallationId.ReadIdentifierFromFile();
			if (!string.IsNullOrEmpty(this.Identifier))
			{
				return;
			}
			string userId = this.UnityAnalyticsIdentifierProvider.UserId;
			string userId2 = this.UnityAdsIdentifierProvider.UserId;
			if (!string.IsNullOrEmpty(userId))
			{
				this.Identifier = userId;
			}
			else if (!string.IsNullOrEmpty(userId2))
			{
				this.Identifier = userId2;
			}
			else
			{
				this.Identifier = InstallationId.GenerateGuid();
			}
			InstallationId.WriteIdentifierToFile(this.Identifier);
			if (string.IsNullOrEmpty(userId))
			{
				this.UnityAnalyticsIdentifierProvider.UserId = this.Identifier;
			}
			if (string.IsNullOrEmpty(userId2))
			{
				this.UnityAdsIdentifierProvider.UserId = this.Identifier;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002134 File Offset: 0x00000334
		private static string ReadIdentifierFromFile()
		{
			return PlayerPrefs.GetString("UnityInstallationId");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002140 File Offset: 0x00000340
		private static void WriteIdentifierToFile(string identifier)
		{
			PlayerPrefs.SetString("UnityInstallationId", identifier);
			PlayerPrefs.Save();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002154 File Offset: 0x00000354
		private static string GenerateGuid()
		{
			return Guid.NewGuid().ToString();
		}

		// Token: 0x04000001 RID: 1
		private const string k_UnityInstallationIdKey = "UnityInstallationId";

		// Token: 0x04000002 RID: 2
		internal string Identifier;

		// Token: 0x04000003 RID: 3
		internal IUserIdentifierProvider UnityAdsIdentifierProvider;

		// Token: 0x04000004 RID: 4
		internal IUserIdentifierProvider UnityAnalyticsIdentifierProvider;
	}
}
