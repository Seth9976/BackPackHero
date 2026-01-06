using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020001DD RID: 477
	internal sealed class SqlStream : Stream
	{
		// Token: 0x060016D8 RID: 5848 RVA: 0x0006FAE2 File Offset: 0x0006DCE2
		internal SqlStream(SqlDataReader reader, bool addByteOrderMark, bool processAllRows)
			: this(0, reader, addByteOrderMark, processAllRows, true)
		{
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0006FAEF File Offset: 0x0006DCEF
		internal SqlStream(int columnOrdinal, SqlDataReader reader, bool addByteOrderMark, bool processAllRows, bool advanceReader)
		{
			this._columnOrdinal = columnOrdinal;
			this._reader = reader;
			this._bom = (addByteOrderMark ? 65279 : 0);
			this._processAllRows = processAllRows;
			this._advanceReader = advanceReader;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x00060F32 File Offset: 0x0005F132
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x00060F32 File Offset: 0x0005F132
		// (set) Token: 0x060016DF RID: 5855 RVA: 0x00060F32 File Offset: 0x0005F132
		public override long Position
		{
			get
			{
				throw ADP.NotSupported();
			}
			set
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0006FB28 File Offset: 0x0006DD28
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._advanceReader && this._reader != null && !this._reader.IsClosed)
				{
					this._reader.Close();
				}
				this._reader = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00060F32 File Offset: 0x0005F132
		public override void Flush()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0006FB84 File Offset: 0x0006DD84
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			int num2 = 0;
			if (this._reader == null)
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
			if (this._bom > 0)
			{
				this._bufferedData = new byte[2];
				num2 = this.ReadBytes(this._bufferedData, 0, 2);
				if (num2 < 2 || (this._bufferedData[0] == 223 && this._bufferedData[1] == 255))
				{
					this._bom = 0;
				}
				while (count > 0 && this._bom > 0)
				{
					buffer[offset] = (byte)this._bom;
					this._bom >>= 8;
					offset++;
					count--;
					num++;
				}
			}
			if (num2 > 0)
			{
				while (count > 0)
				{
					buffer[offset++] = this._bufferedData[0];
					num++;
					count--;
					if (num2 > 1 && count > 0)
					{
						buffer[offset++] = this._bufferedData[1];
						num++;
						count--;
						break;
					}
				}
				this._bufferedData = null;
			}
			return num + this.ReadBytes(buffer, offset, count);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0006FCC8 File Offset: 0x0006DEC8
		private static bool AdvanceToNextRow(SqlDataReader reader)
		{
			while (!reader.Read())
			{
				if (!reader.NextResult())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0006FCE0 File Offset: 0x0006DEE0
		private int ReadBytes(byte[] buffer, int offset, int count)
		{
			bool flag = true;
			int num = 0;
			if (this._reader.IsClosed || this._endOfColumn)
			{
				return 0;
			}
			try
			{
				while (count > 0)
				{
					if (this._advanceReader && this._bytesCol == 0L)
					{
						flag = false;
						if ((!this._readFirstRow || this._processAllRows) && SqlStream.AdvanceToNextRow(this._reader))
						{
							this._readFirstRow = true;
							if (this._reader.IsDBNull(this._columnOrdinal))
							{
								continue;
							}
							flag = true;
						}
					}
					if (!flag)
					{
						break;
					}
					int num2 = (int)this._reader.GetBytesInternal(this._columnOrdinal, this._bytesCol, buffer, offset, count);
					if (num2 < count)
					{
						this._bytesCol = 0L;
						flag = false;
						if (!this._advanceReader)
						{
							this._endOfColumn = true;
						}
					}
					else
					{
						this._bytesCol += (long)num2;
					}
					count -= num2;
					offset += num2;
					num += num2;
				}
				if (!flag && this._advanceReader)
				{
					this._reader.Close();
				}
			}
			catch (Exception ex)
			{
				if (this._advanceReader && ADP.IsCatchableExceptionType(ex))
				{
					this._reader.Close();
				}
				throw;
			}
			return num;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0006FE04 File Offset: 0x0006E004
		internal XmlReader ToXmlReader(bool async = false)
		{
			return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(this, true, async);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00060F32 File Offset: 0x0005F132
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00060F32 File Offset: 0x0005F132
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00060F32 File Offset: 0x0005F132
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x04000F1E RID: 3870
		private SqlDataReader _reader;

		// Token: 0x04000F1F RID: 3871
		private int _columnOrdinal;

		// Token: 0x04000F20 RID: 3872
		private long _bytesCol;

		// Token: 0x04000F21 RID: 3873
		private int _bom;

		// Token: 0x04000F22 RID: 3874
		private byte[] _bufferedData;

		// Token: 0x04000F23 RID: 3875
		private bool _processAllRows;

		// Token: 0x04000F24 RID: 3876
		private bool _advanceReader;

		// Token: 0x04000F25 RID: 3877
		private bool _readFirstRow;

		// Token: 0x04000F26 RID: 3878
		private bool _endOfColumn;
	}
}
