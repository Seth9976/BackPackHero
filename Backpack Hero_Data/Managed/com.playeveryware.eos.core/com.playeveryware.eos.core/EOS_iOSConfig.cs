using System;
using System.Collections.Generic;

namespace PlayEveryWare.EpicOnlineServices
{
	// Token: 0x0200001D RID: 29
	public class EOS_iOSConfig : ICloneableGeneric<EOS_iOSConfig>, IEmpty
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000026E1 File Offset: 0x000008E1
		public EOS_iOSConfig Clone()
		{
			return (EOS_iOSConfig)base.MemberwiseClone();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000026EE File Offset: 0x000008EE
		public bool IsEmpty()
		{
			return EmptyPredicates.IsEmptyOrNullOrContainsOnlyEmpty(this.flags) && EmptyPredicates.IsEmptyOrNull(this.overrideValues);
		}

		// Token: 0x04000030 RID: 48
		public List<string> flags;

		// Token: 0x04000031 RID: 49
		public EOSConfig overrideValues;
	}
}
