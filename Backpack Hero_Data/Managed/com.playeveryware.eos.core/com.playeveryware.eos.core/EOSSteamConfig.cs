using System;
using System.Collections.Generic;

namespace PlayEveryWare.EpicOnlineServices
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public class EOSSteamConfig : ICloneableGeneric<EOSSteamConfig>, IEmpty
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000024D4 File Offset: 0x000006D4
		public EOSSteamConfig Clone()
		{
			return (EOSSteamConfig)base.MemberwiseClone();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000024E1 File Offset: 0x000006E1
		public bool IsEmpty()
		{
			return EmptyPredicates.IsEmptyOrNullOrContainsOnlyEmpty(this.flags) && EmptyPredicates.IsEmptyOrNull(this.overrideLibraryPath) && this.steamSDKMajorVersion == 0U && this.steamSDKMinorVersion == 0U;
		}

		// Token: 0x04000028 RID: 40
		public List<string> flags;

		// Token: 0x04000029 RID: 41
		public string overrideLibraryPath;

		// Token: 0x0400002A RID: 42
		public uint steamSDKMajorVersion;

		// Token: 0x0400002B RID: 43
		public uint steamSDKMinorVersion;
	}
}
