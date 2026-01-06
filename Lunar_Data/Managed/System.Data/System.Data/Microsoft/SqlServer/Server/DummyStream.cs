using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003BE RID: 958
	internal sealed class DummyStream : Stream
	{
		// Token: 0x06002ED8 RID: 11992 RVA: 0x000CADEF File Offset: 0x000C8FEF
		private void DontDoIt()
		{
			throw new Exception(SR.GetString("Internal Error"));
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002EDC RID: 11996 RVA: 0x000CAE00 File Offset: 0x000C9000
		// (set) Token: 0x06002EDD RID: 11997 RVA: 0x000CAE08 File Offset: 0x000C9008
		public override long Position
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002EDE RID: 11998 RVA: 0x000CAE00 File Offset: 0x000C9000
		public override long Length
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000CAE08 File Offset: 0x000C9008
		public override void SetLength(long value)
		{
			this._size = value;
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000CAE11 File Offset: 0x000C9011
		public override long Seek(long value, SeekOrigin loc)
		{
			this.DontDoIt();
			return -1L;
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000094D4 File Offset: 0x000076D4
		public override void Flush()
		{
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000CAE1B File Offset: 0x000C901B
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.DontDoIt();
			return -1;
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000CAE24 File Offset: 0x000C9024
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._size += (long)count;
		}

		// Token: 0x04001BB1 RID: 7089
		private long _size;
	}
}
