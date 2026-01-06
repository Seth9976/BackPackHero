using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003A2 RID: 930
	internal class SmiSettersStream : Stream
	{
		// Token: 0x06002CBB RID: 11451 RVA: 0x000C2588 File Offset: 0x000C0788
		internal SmiSettersStream(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData)
		{
			this._sink = sink;
			this._setters = setters;
			this._ordinal = ordinal;
			this._lengthWritten = 0L;
			this._metaData = metaData;
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000C25B5 File Offset: 0x000C07B5
		public override long Length
		{
			get
			{
				return this._lengthWritten;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000C25B5 File Offset: 0x000C07B5
		// (set) Token: 0x06002CC1 RID: 11457 RVA: 0x000C13A0 File Offset: 0x000BF5A0
		public override long Position
		{
			get
			{
				return this._lengthWritten;
			}
			set
			{
				throw SQL.StreamSeekNotSupported();
			}
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000C25BD File Offset: 0x000C07BD
		public override void Flush()
		{
			this._lengthWritten = ValueUtilsSmi.SetBytesLength(this._sink, this._setters, this._ordinal, this._metaData, this._lengthWritten);
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000C13A0 File Offset: 0x000BF5A0
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw SQL.StreamSeekNotSupported();
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000C25E8 File Offset: 0x000C07E8
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw ADP.ArgumentOutOfRange("value");
			}
			ValueUtilsSmi.SetBytesLength(this._sink, this._setters, this._ordinal, this._metaData, value);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000C2619 File Offset: 0x000C0819
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw SQL.StreamReadNotSupported();
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000C2620 File Offset: 0x000C0820
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._lengthWritten += ValueUtilsSmi.SetBytes(this._sink, this._setters, this._ordinal, this._metaData, this._lengthWritten, buffer, offset, count);
		}

		// Token: 0x04001B2C RID: 6956
		private SmiEventSink_Default _sink;

		// Token: 0x04001B2D RID: 6957
		private ITypedSettersV3 _setters;

		// Token: 0x04001B2E RID: 6958
		private int _ordinal;

		// Token: 0x04001B2F RID: 6959
		private long _lengthWritten;

		// Token: 0x04001B30 RID: 6960
		private SmiMetaData _metaData;
	}
}
