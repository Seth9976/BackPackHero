using System;
using Unity.Services.Core.Device.Internal;
using UnityEngine;

namespace Unity.Services.Analytics
{
	// Token: 0x02000018 RID: 24
	internal class InternalNewPlayerHelper
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003000 File Offset: 0x00001200
		internal IInstallationId InstallId { get; }

		// Token: 0x06000044 RID: 68 RVA: 0x00003008 File Offset: 0x00001208
		internal InternalNewPlayerHelper(IInstallationId installId)
		{
			if (installId == null)
			{
				throw new ArgumentNullException("Did not get IInstallationId provider from Unity Services Core.");
			}
			this.InstallId = installId;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003028 File Offset: 0x00001228
		internal bool IsNewPlayer()
		{
			string orCreateIdentifier = this.InstallId.GetOrCreateIdentifier();
			string text = this.ReadAnalyticsIdentifier();
			if (string.IsNullOrEmpty(text) || text != orCreateIdentifier)
			{
				this.WriteAnalyticsIdentifierToFile(orCreateIdentifier);
				return true;
			}
			return false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003063 File Offset: 0x00001263
		internal string ReadAnalyticsIdentifier()
		{
			return PlayerPrefs.GetString("UnityAnalyticsInstallationId");
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000306F File Offset: 0x0000126F
		internal void WriteAnalyticsIdentifierToFile(string identifier)
		{
			PlayerPrefs.SetString("UnityAnalyticsInstallationId", identifier);
			PlayerPrefs.Save();
		}

		// Token: 0x04000088 RID: 136
		private const string k_UnityAnalyticsInstallationIdKey = "UnityAnalyticsInstallationId";
	}
}
