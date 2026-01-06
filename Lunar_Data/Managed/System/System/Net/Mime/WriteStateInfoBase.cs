using System;

namespace System.Net.Mime
{
	// Token: 0x02000622 RID: 1570
	internal class WriteStateInfoBase
	{
		// Token: 0x06003257 RID: 12887 RVA: 0x000B4DE8 File Offset: 0x000B2FE8
		internal WriteStateInfoBase()
		{
			this._header = Array.Empty<byte>();
			this._footer = Array.Empty<byte>();
			this._maxLineLength = 70;
			this._buffer = new byte[1024];
			this._currentLineLength = 0;
			this._currentBufferUsed = 0;
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000B4E37 File Offset: 0x000B3037
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength)
			: this(bufferSize, header, footer, maxLineLength, 0)
		{
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x000B4E45 File Offset: 0x000B3045
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength)
		{
			this._buffer = new byte[bufferSize];
			this._header = header;
			this._footer = footer;
			this._maxLineLength = maxLineLength;
			this._currentLineLength = mimeHeaderLength;
			this._currentBufferUsed = 0;
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000B4E7E File Offset: 0x000B307E
		internal int FooterLength
		{
			get
			{
				return this._footer.Length;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000B4E88 File Offset: 0x000B3088
		internal byte[] Footer
		{
			get
			{
				return this._footer;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x000B4E90 File Offset: 0x000B3090
		internal byte[] Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000B4E98 File Offset: 0x000B3098
		internal byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000B4EA0 File Offset: 0x000B30A0
		internal int Length
		{
			get
			{
				return this._currentBufferUsed;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x000B4EA8 File Offset: 0x000B30A8
		internal int CurrentLineLength
		{
			get
			{
				return this._currentLineLength;
			}
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x000B4EB0 File Offset: 0x000B30B0
		private void EnsureSpaceInBuffer(int moreBytes)
		{
			int num = this.Buffer.Length;
			while (this._currentBufferUsed + moreBytes >= num)
			{
				num *= 2;
			}
			if (num > this.Buffer.Length)
			{
				byte[] array = new byte[num];
				this._buffer.CopyTo(array, 0);
				this._buffer = array;
			}
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000B4F00 File Offset: 0x000B3100
		internal void Append(byte aByte)
		{
			this.EnsureSpaceInBuffer(1);
			byte[] buffer = this.Buffer;
			int currentBufferUsed = this._currentBufferUsed;
			this._currentBufferUsed = currentBufferUsed + 1;
			buffer[currentBufferUsed] = aByte;
			this._currentLineLength++;
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x000B4F3B File Offset: 0x000B313B
		internal void Append(params byte[] bytes)
		{
			this.EnsureSpaceInBuffer(bytes.Length);
			bytes.CopyTo(this._buffer, this.Length);
			this._currentLineLength += bytes.Length;
			this._currentBufferUsed += bytes.Length;
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x000B4F78 File Offset: 0x000B3178
		internal void AppendCRLF(bool includeSpace)
		{
			this.AppendFooter();
			this.Append(new byte[] { 13, 10 });
			this._currentLineLength = 0;
			if (includeSpace)
			{
				this.Append(32);
			}
			this.AppendHeader();
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000B4FAE File Offset: 0x000B31AE
		internal void AppendHeader()
		{
			if (this.Header != null && this.Header.Length != 0)
			{
				this.Append(this.Header);
			}
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000B4FCD File Offset: 0x000B31CD
		internal void AppendFooter()
		{
			if (this.Footer != null && this.Footer.Length != 0)
			{
				this.Append(this.Footer);
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x000B4FEC File Offset: 0x000B31EC
		internal int MaxLineLength
		{
			get
			{
				return this._maxLineLength;
			}
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000B4FF4 File Offset: 0x000B31F4
		internal void Reset()
		{
			this._currentBufferUsed = 0;
			this._currentLineLength = 0;
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000B5004 File Offset: 0x000B3204
		internal void BufferFlushed()
		{
			this._currentBufferUsed = 0;
		}

		// Token: 0x04001ECA RID: 7882
		protected readonly byte[] _header;

		// Token: 0x04001ECB RID: 7883
		protected readonly byte[] _footer;

		// Token: 0x04001ECC RID: 7884
		protected readonly int _maxLineLength;

		// Token: 0x04001ECD RID: 7885
		protected byte[] _buffer;

		// Token: 0x04001ECE RID: 7886
		protected int _currentLineLength;

		// Token: 0x04001ECF RID: 7887
		protected int _currentBufferUsed;

		// Token: 0x04001ED0 RID: 7888
		protected const int DefaultBufferSize = 1024;
	}
}
