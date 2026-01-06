using System;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Data.SqlTypes
{
	// Token: 0x020002B9 RID: 697
	internal sealed class StreamOnSqlBytes : Stream
	{
		// Token: 0x06001F0C RID: 7948 RVA: 0x00094F2B File Offset: 0x0009312B
		internal StreamOnSqlBytes(SqlBytes sb)
		{
			this._sb = sb;
			this._lPosition = 0L;
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x00094F42 File Offset: 0x00093142
		public override bool CanRead
		{
			get
			{
				return this._sb != null && !this._sb.IsNull;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00094F5C File Offset: 0x0009315C
		public override bool CanSeek
		{
			get
			{
				return this._sb != null;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x00094F67 File Offset: 0x00093167
		public override bool CanWrite
		{
			get
			{
				return this._sb != null && (!this._sb.IsNull || this._sb._rgbBuf != null);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x00094F90 File Offset: 0x00093190
		public override long Length
		{
			get
			{
				this.CheckIfStreamClosed("get_Length");
				return this._sb.Length;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x00094FA8 File Offset: 0x000931A8
		// (set) Token: 0x06001F12 RID: 7954 RVA: 0x00094FBB File Offset: 0x000931BB
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
				if (value < 0L || value > this._sb.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lPosition = value;
			}
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00094FF0 File Offset: 0x000931F0
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckIfStreamClosed("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this._sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this._lPosition + offset;
				if (num < 0L || num > this._sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this._sb.Length + offset;
				if (num < 0L || num > this._sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = num;
				break;
			}
			default:
				throw ADP.InvalidSeekOrigin("offset");
			}
			return this._lPosition;
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000950C0 File Offset: 0x000932C0
		public override int Read(byte[] buffer, int offset, int count)
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
			int num = (int)this._sb.Read(this._lPosition, buffer, offset, count);
			this._lPosition += (long)num;
			return num;
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00095138 File Offset: 0x00093338
		public override void Write(byte[] buffer, int offset, int count)
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
			this._sb.Write(this._lPosition, buffer, offset, count);
			this._lPosition += (long)count;
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x000951B0 File Offset: 0x000933B0
		public override int ReadByte()
		{
			this.CheckIfStreamClosed("ReadByte");
			if (this._lPosition >= this._sb.Length)
			{
				return -1;
			}
			int num = (int)this._sb[this._lPosition];
			this._lPosition += 1L;
			return num;
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x000951FD File Offset: 0x000933FD
		public override void WriteByte(byte value)
		{
			this.CheckIfStreamClosed("WriteByte");
			this._sb[this._lPosition] = value;
			this._lPosition += 1L;
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0009522B File Offset: 0x0009342B
		public override void SetLength(long value)
		{
			this.CheckIfStreamClosed("SetLength");
			this._sb.SetLength(value);
			if (this._lPosition > value)
			{
				this._lPosition = value;
			}
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00095254 File Offset: 0x00093454
		public override void Flush()
		{
			if (this._sb.FStream())
			{
				this._sb._stream.Flush();
			}
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x00095274 File Offset: 0x00093474
		protected override void Dispose(bool disposing)
		{
			try
			{
				this._sb = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000952A4 File Offset: 0x000934A4
		private bool FClosed()
		{
			return this._sb == null;
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000952AF File Offset: 0x000934AF
		private void CheckIfStreamClosed([CallerMemberName] string methodname = "")
		{
			if (this.FClosed())
			{
				throw ADP.StreamClosed(methodname);
			}
		}

		// Token: 0x0400162E RID: 5678
		private SqlBytes _sb;

		// Token: 0x0400162F RID: 5679
		private long _lPosition;
	}
}
