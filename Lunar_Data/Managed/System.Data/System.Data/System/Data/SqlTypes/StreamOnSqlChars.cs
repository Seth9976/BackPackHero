using System;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Data.SqlTypes
{
	// Token: 0x020002BB RID: 699
	internal sealed class StreamOnSqlChars : SqlStreamChars
	{
		// Token: 0x06001F3C RID: 7996 RVA: 0x000959FC File Offset: 0x00093BFC
		internal StreamOnSqlChars(SqlChars s)
		{
			this._sqlchars = s;
			this._lPosition = 0L;
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x00095A13 File Offset: 0x00093C13
		public override bool IsNull
		{
			get
			{
				return this._sqlchars == null || this._sqlchars.IsNull;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x00095A2A File Offset: 0x00093C2A
		public override long Length
		{
			get
			{
				this.CheckIfStreamClosed("get_Length");
				return this._sqlchars.Length;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x00095A42 File Offset: 0x00093C42
		// (set) Token: 0x06001F40 RID: 8000 RVA: 0x00095A55 File Offset: 0x00093C55
		public override long Position
		{
			get
			{
				this.CheckIfStreamClosed("get_Position");
				return this._lPosition;
			}
			set
			{
				this.CheckIfStreamClosed("set_Position");
				if (value < 0L || value > this._sqlchars.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lPosition = value;
			}
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x00095A88 File Offset: 0x00093C88
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckIfStreamClosed("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this._sqlchars.Length)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				this._lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this._lPosition + offset;
				if (num < 0L || num > this._sqlchars.Length)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				this._lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this._sqlchars.Length + offset;
				if (num < 0L || num > this._sqlchars.Length)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				this._lPosition = num;
				break;
			}
			default:
				throw ADP.ArgumentOutOfRange("offset");
			}
			return this._lPosition;
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00095B58 File Offset: 0x00093D58
		public override int Read(char[] buffer, int offset, int count)
		{
			this.CheckIfStreamClosed("Read");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = (int)this._sqlchars.Read(this._lPosition, buffer, offset, count);
			this._lPosition += (long)num;
			return num;
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00095BD0 File Offset: 0x00093DD0
		public override void Write(char[] buffer, int offset, int count)
		{
			this.CheckIfStreamClosed("Write");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._sqlchars.Write(this._lPosition, buffer, offset, count);
			this._lPosition += (long)count;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00095C45 File Offset: 0x00093E45
		public override void SetLength(long value)
		{
			this.CheckIfStreamClosed("SetLength");
			this._sqlchars.SetLength(value);
			if (this._lPosition > value)
			{
				this._lPosition = value;
			}
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00095C6E File Offset: 0x00093E6E
		protected override void Dispose(bool disposing)
		{
			this._sqlchars = null;
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00095C77 File Offset: 0x00093E77
		private bool FClosed()
		{
			return this._sqlchars == null;
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x00095C82 File Offset: 0x00093E82
		private void CheckIfStreamClosed([CallerMemberName] string methodname = "")
		{
			if (this.FClosed())
			{
				throw ADP.StreamClosed(methodname);
			}
		}

		// Token: 0x04001637 RID: 5687
		private SqlChars _sqlchars;

		// Token: 0x04001638 RID: 5688
		private long _lPosition;
	}
}
