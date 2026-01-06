using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x02000609 RID: 1545
	internal class EightBitStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x000B2088 File Offset: 0x000B0288
		private WriteStateInfoBase WriteState
		{
			get
			{
				WriteStateInfoBase writeStateInfoBase;
				if ((writeStateInfoBase = this._writeState) == null)
				{
					writeStateInfoBase = (this._writeState = new WriteStateInfoBase());
				}
				return writeStateInfoBase;
			}
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000B20AD File Offset: 0x000B02AD
		internal EightBitStream(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000B20B6 File Offset: 0x000B02B6
		internal EightBitStream(Stream stream, bool shouldEncodeLeadingDots)
			: this(stream)
		{
			this._shouldEncodeLeadingDots = shouldEncodeLeadingDots;
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000B20C8 File Offset: 0x000B02C8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			IAsyncResult asyncResult;
			if (this._shouldEncodeLeadingDots)
			{
				this.EncodeLines(buffer, offset, count);
				asyncResult = base.BeginWrite(this.WriteState.Buffer, 0, this.WriteState.Length, callback, state);
			}
			else
			{
				asyncResult = base.BeginWrite(buffer, offset, count, callback, state);
			}
			return asyncResult;
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000B214F File Offset: 0x000B034F
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.EndWrite(asyncResult);
			this.WriteState.BufferFlushed();
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000B2164 File Offset: 0x000B0364
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this._shouldEncodeLeadingDots)
			{
				this.EncodeLines(buffer, offset, count);
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.BufferFlushed();
				return;
			}
			base.Write(buffer, offset, count);
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000B21EC File Offset: 0x000B03EC
		private void EncodeLines(byte[] buffer, int offset, int count)
		{
			int num = offset;
			while (num < offset + count && num < buffer.Length)
			{
				if (buffer[num] == 13 && num + 1 < offset + count && buffer[num + 1] == 10)
				{
					this.WriteState.AppendCRLF(false);
					num++;
				}
				else if (this.WriteState.CurrentLineLength == 0 && buffer[num] == 46)
				{
					this.WriteState.Append(46);
					this.WriteState.Append(buffer[num]);
				}
				else
				{
					this.WriteState.Append(buffer[num]);
				}
				num++;
			}
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x00007575 File Offset: 0x00005775
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x0000822E File Offset: 0x0000642E
		public int DecodeBytes(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x0000822E File Offset: 0x0000642E
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x0000822E File Offset: 0x0000642E
		public string GetEncodedString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001E59 RID: 7769
		private WriteStateInfoBase _writeState;

		// Token: 0x04001E5A RID: 7770
		private bool _shouldEncodeLeadingDots;
	}
}
