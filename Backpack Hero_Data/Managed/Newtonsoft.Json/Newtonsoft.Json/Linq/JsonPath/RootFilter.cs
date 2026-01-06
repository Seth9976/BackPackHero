using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000D9 RID: 217
	[NullableContext(1)]
	[Nullable(0)]
	internal class RootFilter : PathFilter
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x0002FDC3 File Offset: 0x0002DFC3
		private RootFilter()
		{
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002FDCB File Offset: 0x0002DFCB
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			return new JToken[] { root };
		}

		// Token: 0x040003E2 RID: 994
		public static readonly RootFilter Instance = new RootFilter();
	}
}
