using System;
using UnityEngine;

namespace Unity.Services.Core.Device
{
	// Token: 0x02000005 RID: 5
	internal class UnityAnalyticsIdentifier : IUserIdentifierProvider
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002181 File Offset: 0x00000381
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002190 File Offset: 0x00000390
		public string UserId
		{
			get
			{
				return PlayerPrefs.GetString("unity.cloud_userid");
			}
			set
			{
				try
				{
					PlayerPrefs.SetString("unity.cloud_userid", value);
					PlayerPrefs.Save();
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x04000005 RID: 5
		private const string k_PlayerUserIdKey = "unity.cloud_userid";
	}
}
