using System;

namespace System
{
	// Token: 0x0200001D RID: 29
	internal static class NotImplemented
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000571B File Offset: 0x0000391B
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005722 File Offset: 0x00003922
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000571B File Offset: 0x0000391B
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
