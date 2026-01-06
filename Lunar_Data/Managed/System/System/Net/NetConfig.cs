using System;

namespace System.Net
{
	// Token: 0x020004BA RID: 1210
	internal class NetConfig : ICloneable
	{
		// Token: 0x060026F5 RID: 9973 RVA: 0x00090BE2 File Offset: 0x0008EDE2
		internal NetConfig()
		{
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x00090BF2 File Offset: 0x0008EDF2
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x040016A3 RID: 5795
		internal bool ipv6Enabled;

		// Token: 0x040016A4 RID: 5796
		internal int MaxResponseHeadersLength = 64;
	}
}
