using System;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x020002CE RID: 718
	internal abstract class SqlStreamChars : INullable, IDisposable
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060021F4 RID: 8692
		public abstract bool IsNull { get; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060021F5 RID: 8693
		public abstract long Length { get; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060021F6 RID: 8694
		// (set) Token: 0x060021F7 RID: 8695
		public abstract long Position { get; set; }

		// Token: 0x060021F8 RID: 8696
		public abstract int Read(char[] buffer, int offset, int count);

		// Token: 0x060021F9 RID: 8697
		public abstract void Write(char[] buffer, int offset, int count);

		// Token: 0x060021FA RID: 8698
		public abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x060021FB RID: 8699
		public abstract void SetLength(long value);

		// Token: 0x060021FC RID: 8700 RVA: 0x0009DAAE File Offset: 0x0009BCAE
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000094D4 File Offset: 0x000076D4
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
