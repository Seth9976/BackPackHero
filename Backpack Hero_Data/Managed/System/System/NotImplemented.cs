using System;

namespace System
{
	// Token: 0x02000143 RID: 323
	internal static class NotImplemented
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0001FC8B File Offset: 0x0001DE8B
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001FC92 File Offset: 0x0001DE92
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001FC8B File Offset: 0x0001DE8B
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
