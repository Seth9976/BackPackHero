using System;
using System.Collections.Generic;

namespace PlayEveryWare.EpicOnlineServices
{
	// Token: 0x02000008 RID: 8
	public class EOSAndroidConfig : ICloneableGeneric<EOSAndroidConfig>, IEmpty
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000021B7 File Offset: 0x000003B7
		public EOSAndroidConfig Clone()
		{
			return (EOSAndroidConfig)base.MemberwiseClone();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021C4 File Offset: 0x000003C4
		public bool IsEmpty()
		{
			return EmptyPredicates.IsEmptyOrNullOrContainsOnlyEmpty(this.flags) && EmptyPredicates.IsEmptyOrNull(this.overrideValues);
		}

		// Token: 0x04000009 RID: 9
		public List<string> flags;

		// Token: 0x0400000A RID: 10
		public EOSConfig overrideValues;
	}
}
