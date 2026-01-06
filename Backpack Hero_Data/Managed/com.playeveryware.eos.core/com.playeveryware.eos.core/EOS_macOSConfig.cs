using System;
using System.Collections.Generic;

namespace PlayEveryWare.EpicOnlineServices
{
	// Token: 0x0200001F RID: 31
	public class EOS_macOSConfig : ICloneableGeneric<EOS_macOSConfig>, IEmpty
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002743 File Offset: 0x00000943
		public EOS_macOSConfig Clone()
		{
			return (EOS_macOSConfig)base.MemberwiseClone();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002750 File Offset: 0x00000950
		public bool IsEmpty()
		{
			return EmptyPredicates.IsEmptyOrNullOrContainsOnlyEmpty(this.flags) && EmptyPredicates.IsEmptyOrNull(this.overrideValues);
		}

		// Token: 0x04000034 RID: 52
		public List<string> flags;

		// Token: 0x04000035 RID: 53
		public EOSConfig overrideValues;
	}
}
