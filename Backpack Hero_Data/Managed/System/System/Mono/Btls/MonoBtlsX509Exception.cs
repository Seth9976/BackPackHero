using System;

namespace Mono.Btls
{
	// Token: 0x020000F8 RID: 248
	internal class MonoBtlsX509Exception : Exception
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00010584 File Offset: 0x0000E784
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0001058C File Offset: 0x0000E78C
		public MonoBtlsX509Error ErrorCode { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00010595 File Offset: 0x0000E795
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0001059D File Offset: 0x0000E79D
		public string ErrorMessage { get; private set; }

		// Token: 0x060005B7 RID: 1463 RVA: 0x000105A6 File Offset: 0x0000E7A6
		public MonoBtlsX509Exception(MonoBtlsX509Error code, string message)
			: base(message)
		{
			this.ErrorCode = code;
			this.ErrorMessage = message;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000105BD File Offset: 0x0000E7BD
		public override string ToString()
		{
			return string.Format("[MonoBtlsX509Exception: ErrorCode={0}, ErrorMessage={1}]", this.ErrorCode, this.ErrorMessage);
		}
	}
}
