using System;
using System.Data.SqlClient;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000394 RID: 916
	internal class SmiGettersStream : Stream
	{
		// Token: 0x06002C19 RID: 11289 RVA: 0x000C1338 File Offset: 0x000BF538
		internal SmiGettersStream(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			this._sink = sink;
			this._getters = getters;
			this._ordinal = ordinal;
			this._readPosition = 0L;
			this._metaData = metaData;
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x000C1368 File Offset: 0x000BF568
		public override long Length
		{
			get
			{
				return ValueUtilsSmi.GetBytesInternal(this._sink, this._getters, this._ordinal, this._metaData, 0L, null, 0, 0, false);
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x000C1398 File Offset: 0x000BF598
		// (set) Token: 0x06002C1F RID: 11295 RVA: 0x000C13A0 File Offset: 0x000BF5A0
		public override long Position
		{
			get
			{
				return this._readPosition;
			}
			set
			{
				throw SQL.StreamSeekNotSupported();
			}
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000C13A7 File Offset: 0x000BF5A7
		public override void Flush()
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000C13A0 File Offset: 0x000BF5A0
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw SQL.StreamSeekNotSupported();
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000C13A7 File Offset: 0x000BF5A7
		public override void SetLength(long value)
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000C13B0 File Offset: 0x000BF5B0
		public override int Read(byte[] buffer, int offset, int count)
		{
			long bytesInternal = ValueUtilsSmi.GetBytesInternal(this._sink, this._getters, this._ordinal, this._metaData, this._readPosition, buffer, offset, count, false);
			this._readPosition += bytesInternal;
			return checked((int)bytesInternal);
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000C13A7 File Offset: 0x000BF5A7
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x04001AC9 RID: 6857
		private SmiEventSink_Default _sink;

		// Token: 0x04001ACA RID: 6858
		private ITypedGettersV3 _getters;

		// Token: 0x04001ACB RID: 6859
		private int _ordinal;

		// Token: 0x04001ACC RID: 6860
		private long _readPosition;

		// Token: 0x04001ACD RID: 6861
		private SmiMetaData _metaData;
	}
}
