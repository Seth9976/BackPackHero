using System;

namespace System.Net
{
	// Token: 0x02000439 RID: 1081
	internal class HeaderInfo
	{
		// Token: 0x06002244 RID: 8772 RVA: 0x0007DFED File Offset: 0x0007C1ED
		internal HeaderInfo(string name, bool requestRestricted, bool responseRestricted, bool multi, HeaderParser p)
		{
			this.HeaderName = name;
			this.IsRequestRestricted = requestRestricted;
			this.IsResponseRestricted = responseRestricted;
			this.Parser = p;
			this.AllowMultiValues = multi;
		}

		// Token: 0x040013F0 RID: 5104
		internal readonly bool IsRequestRestricted;

		// Token: 0x040013F1 RID: 5105
		internal readonly bool IsResponseRestricted;

		// Token: 0x040013F2 RID: 5106
		internal readonly HeaderParser Parser;

		// Token: 0x040013F3 RID: 5107
		internal readonly string HeaderName;

		// Token: 0x040013F4 RID: 5108
		internal readonly bool AllowMultiValues;
	}
}
