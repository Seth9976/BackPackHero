using System;
using Unity.Services.Analytics.Internal;

namespace Unity.Services.Analytics.Data
{
	// Token: 0x02000043 RID: 67
	internal class StdCommonParams
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005124 File Offset: 0x00003324
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000512C File Offset: 0x0000332C
		internal string GameStoreID { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005135 File Offset: 0x00003335
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000513D File Offset: 0x0000333D
		internal string GameBundleID { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005146 File Offset: 0x00003346
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000514E File Offset: 0x0000334E
		internal string Platform { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005157 File Offset: 0x00003357
		// (set) Token: 0x0600015B RID: 347 RVA: 0x0000515F File Offset: 0x0000335F
		internal string UasUserID { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005168 File Offset: 0x00003368
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005170 File Offset: 0x00003370
		internal string Idfv { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005179 File Offset: 0x00003379
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00005181 File Offset: 0x00003381
		internal double? DeviceVolume { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000518A File Offset: 0x0000338A
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00005192 File Offset: 0x00003392
		internal double? BatteryLoad { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000519B File Offset: 0x0000339B
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000051A3 File Offset: 0x000033A3
		internal string BuildGuuid { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000051AC File Offset: 0x000033AC
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000051B4 File Offset: 0x000033B4
		internal string ClientVersion { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000051BD File Offset: 0x000033BD
		// (set) Token: 0x06000167 RID: 359 RVA: 0x000051C5 File Offset: 0x000033C5
		internal string UserCountry { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000051CE File Offset: 0x000033CE
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000051D6 File Offset: 0x000033D6
		internal string ProjectID { get; set; }

		// Token: 0x0600016A RID: 362 RVA: 0x000051E0 File Offset: 0x000033E0
		internal void SerializeCommonEventParams(ref IBuffer buf, string callingMethodIdentifier)
		{
			if (!string.IsNullOrEmpty(this.GameStoreID))
			{
				buf.PushString(this.GameStoreID, "gameStoreID");
			}
			if (!string.IsNullOrEmpty(this.GameBundleID))
			{
				buf.PushString(this.GameBundleID, "gameBundleID");
			}
			if (!string.IsNullOrEmpty(this.Platform))
			{
				buf.PushString(this.Platform, "platform");
			}
			if (!string.IsNullOrEmpty(this.Idfv))
			{
				buf.PushString(this.Idfv, "idfv");
			}
			if (!string.IsNullOrEmpty(this.UasUserID))
			{
				buf.PushString(this.UasUserID, "uasUserID");
			}
			if (!string.IsNullOrEmpty(this.BuildGuuid))
			{
				buf.PushString(this.BuildGuuid, "buildGUUID");
			}
			if (!string.IsNullOrEmpty(this.ClientVersion))
			{
				buf.PushString(this.ClientVersion, "clientVersion");
			}
			if (!string.IsNullOrEmpty(this.UserCountry))
			{
				buf.PushString(this.UserCountry, "userCountry");
			}
			if (this.DeviceVolume != null)
			{
				buf.PushDouble(this.DeviceVolume.Value, "deviceVolume");
			}
			if (this.BatteryLoad != null)
			{
				buf.PushDouble(this.BatteryLoad.Value, "batteryLoad");
			}
			if (!string.IsNullOrEmpty(this.ProjectID))
			{
				buf.PushString(this.ProjectID, "projectID");
			}
			buf.PushString(callingMethodIdentifier, "sdkMethod");
		}
	}
}
