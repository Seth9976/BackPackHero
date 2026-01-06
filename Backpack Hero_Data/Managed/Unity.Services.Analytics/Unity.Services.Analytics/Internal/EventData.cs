using System;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal.Platform;
using Unity.Services.Analytics.Platform;
using UnityEngine;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000038 RID: 56
	public class EventData
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00004D86 File Offset: 0x00002F86
		internal EventData()
		{
			this.Data = new Dictionary<string, object>();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004D99 File Offset: 0x00002F99
		public void Set(string key, float value)
		{
			this.Data[key] = value;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004DAD File Offset: 0x00002FAD
		public void Set(string key, double value)
		{
			this.Data[key] = value;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004DC1 File Offset: 0x00002FC1
		public void Set(string key, bool value)
		{
			this.Data[key] = value;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004DD5 File Offset: 0x00002FD5
		public void Set(string key, int value)
		{
			this.Data[key] = value;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004DE9 File Offset: 0x00002FE9
		public void Set(string key, object value)
		{
			this.Data[key] = value;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public void Set(string key, long value)
		{
			this.Data[key] = value;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004E0C File Offset: 0x0000300C
		public void Set(string key, string value)
		{
			this.Data[key] = value;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004E1B File Offset: 0x0000301B
		public void AddPlatform()
		{
			this.Set("platform", Runtime.Name());
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004E2D File Offset: 0x0000302D
		public void AddBatteryLoad()
		{
			this.Set("batteryLoad", 1.0);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004E44 File Offset: 0x00003044
		public void AddConnectionType()
		{
			this.Set("connectionType", NetworkReachability.ReachableViaLocalAreaNetwork.ToString());
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004E6B File Offset: 0x0000306B
		public void AddUserCountry()
		{
			this.Set("userCountry", UserCountry.Name());
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004E7D File Offset: 0x0000307D
		public void AddBuildGuuid()
		{
			this.Set("buildGUUID", Application.buildGUID);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004E8F File Offset: 0x0000308F
		public void AddClientVersion()
		{
			this.Set("clientVersion", Application.version);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004EA1 File Offset: 0x000030A1
		public void AddGameBundleID()
		{
			this.Set("gameBundleID", Application.identifier);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004EB4 File Offset: 0x000030B4
		public void AddStdParamData(string sdkMethod, string uasID)
		{
			this.AddPlatform();
			this.AddBatteryLoad();
			this.AddConnectionType();
			this.AddUserCountry();
			this.AddBuildGuuid();
			this.AddClientVersion();
			this.AddGameBundleID();
			if (!string.IsNullOrEmpty(sdkMethod))
			{
				this.Set("sdkMethod", sdkMethod);
			}
			if (!string.IsNullOrEmpty(uasID))
			{
				this.Set("uasUserID", uasID);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004F13 File Offset: 0x00003113
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00004F1B File Offset: 0x0000311B
		public Dictionary<string, object> Data { get; private set; }
	}
}
