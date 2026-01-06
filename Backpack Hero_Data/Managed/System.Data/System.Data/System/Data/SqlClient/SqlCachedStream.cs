using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x020001DE RID: 478
	internal sealed class SqlCachedStream : Stream
	{
		// Token: 0x060016E9 RID: 5865 RVA: 0x0006FE0E File Offset: 0x0006E00E
		internal SqlCachedStream(SqlCachedBuffer sqlBuf)
		{
			this._cachedBytes = sqlBuf.CachedBytes;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x0006FE22 File Offset: 0x0006E022
		public override long Length
		{
			get
			{
				return this.TotalLength;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0006FE2C File Offset: 0x0006E02C
		// (set) Token: 0x060016EF RID: 5871 RVA: 0x0006FE73 File Offset: 0x0006E073
		public override long Position
		{
			get
			{
				long num = 0L;
				if (this._currentArrayIndex > 0)
				{
					for (int i = 0; i < this._currentArrayIndex; i++)
					{
						num += (long)this._cachedBytes[i].Length;
					}
				}
				return num + (long)this._currentPosition;
			}
			set
			{
				if (this._cachedBytes == null)
				{
					throw ADP.StreamClosed("set_Position");
				}
				this.SetInternalPosition(value, "set_Position");
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0006FE94 File Offset: 0x0006E094
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._cachedBytes != null)
				{
					this._cachedBytes.Clear();
				}
				this._cachedBytes = null;
				this._currentPosition = 0;
				this._currentArrayIndex = 0;
				this._totalLength = 0L;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00060F32 File Offset: 0x0005F132
		public override void Flush()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0006FEF0 File Offset: 0x0006E0F0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			if (this._cachedBytes == null)
			{
				throw ADP.StreamClosed("Read");
			}
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw ADP.ArgumentOutOfRange(string.Empty, (offset < 0) ? "offset" : "count");
			}
			if (buffer.Length - offset < count)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			if (this._cachedBytes.Count <= this._currentArrayIndex)
			{
				return 0;
			}
			while (count > 0)
			{
				if (this._cachedBytes[this._currentArrayIndex].Length <= this._currentPosition)
				{
					this._currentArrayIndex++;
					if (this._cachedBytes.Count <= this._currentArrayIndex)
					{
						break;
					}
					this._currentPosition = 0;
				}
				int num2 = this._cachedBytes[this._currentArrayIndex].Length - this._currentPosition;
				if (num2 > count)
				{
					num2 = count;
				}
				Buffer.BlockCopy(this._cachedBytes[this._currentArrayIndex], this._currentPosition, buffer, offset, num2);
				this._currentPosition += num2;
				count -= num2;
				offset += num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00070018 File Offset: 0x0006E218
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num = 0L;
			if (this._cachedBytes == null)
			{
				throw ADP.StreamClosed("Seek");
			}
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.SetInternalPosition(offset, "offset");
				break;
			case SeekOrigin.Current:
				num = offset + this.Position;
				this.SetInternalPosition(num, "offset");
				break;
			case SeekOrigin.End:
				num = this.TotalLength + offset;
				this.SetInternalPosition(num, "offset");
				break;
			default:
				throw ADP.InvalidSeekOrigin("offset");
			}
			return num;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00060F32 File Offset: 0x0005F132
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00060F32 File Offset: 0x0005F132
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00070098 File Offset: 0x0006E298
		private void SetInternalPosition(long lPos, string argumentName)
		{
			long num = lPos;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
			for (int i = 0; i < this._cachedBytes.Count; i++)
			{
				if (num <= (long)this._cachedBytes[i].Length)
				{
					this._currentArrayIndex = i;
					this._currentPosition = (int)num;
					return;
				}
				num -= (long)this._cachedBytes[i].Length;
			}
			if (num > 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0007010C File Offset: 0x0006E30C
		private long TotalLength
		{
			get
			{
				if (this._totalLength == 0L && this._cachedBytes != null)
				{
					long num = 0L;
					for (int i = 0; i < this._cachedBytes.Count; i++)
					{
						num += (long)this._cachedBytes[i].Length;
					}
					this._totalLength = num;
				}
				return this._totalLength;
			}
		}

		// Token: 0x04000F27 RID: 3879
		private int _currentPosition;

		// Token: 0x04000F28 RID: 3880
		private int _currentArrayIndex;

		// Token: 0x04000F29 RID: 3881
		private List<byte[]> _cachedBytes;

		// Token: 0x04000F2A RID: 3882
		private long _totalLength;
	}
}
