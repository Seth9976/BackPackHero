using System;

namespace Mono.Btls
{
	// Token: 0x020000DA RID: 218
	internal class MonoBtlsException : Exception
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000DB6E File Offset: 0x0000BD6E
		public MonoBtlsException()
		{
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000DB76 File Offset: 0x0000BD76
		public MonoBtlsException(MonoBtlsSslError error)
			: base(error.ToString())
		{
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000DB8B File Offset: 0x0000BD8B
		public MonoBtlsException(string message)
			: base(message)
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000DB94 File Offset: 0x0000BD94
		public MonoBtlsException(string format, params object[] args)
			: base(string.Format(format, args))
		{
		}
	}
}
