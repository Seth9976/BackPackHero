using System;
using System.Collections.Generic;

namespace PlayEveryWare.EpicOnlineServices
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	public class EOSLinuxConfig : ICloneableGeneric<EOSLinuxConfig>, IEmpty
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002712 File Offset: 0x00000912
		public EOSLinuxConfig Clone()
		{
			return (EOSLinuxConfig)base.MemberwiseClone();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000271F File Offset: 0x0000091F
		public bool IsEmpty()
		{
			return EmptyPredicates.IsEmptyOrNullOrContainsOnlyEmpty(this.flags) && EmptyPredicates.IsEmptyOrNull(this.overrideValues);
		}

		// Token: 0x04000032 RID: 50
		public List<string> flags;

		// Token: 0x04000033 RID: 51
		public EOSConfig overrideValues;
	}
}
