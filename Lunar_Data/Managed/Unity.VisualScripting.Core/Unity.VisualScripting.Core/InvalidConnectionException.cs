using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200002B RID: 43
	public class InvalidConnectionException : Exception
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00004BB9 File Offset: 0x00002DB9
		public InvalidConnectionException()
			: base("")
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00004BC6 File Offset: 0x00002DC6
		public InvalidConnectionException(string message)
			: base(message)
		{
		}
	}
}
