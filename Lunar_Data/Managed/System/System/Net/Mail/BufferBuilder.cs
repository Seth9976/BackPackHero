using System;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000629 RID: 1577
	internal sealed class BufferBuilder
	{
		// Token: 0x0600327A RID: 12922 RVA: 0x000B57DF File Offset: 0x000B39DF
		internal BufferBuilder()
			: this(256)
		{
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000B57EC File Offset: 0x000B39EC
		internal BufferBuilder(int initialSize)
		{
			this._buffer = new byte[initialSize];
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000B5800 File Offset: 0x000B3A00
		private void EnsureBuffer(int count)
		{
			if (count > this._buffer.Length - this._offset)
			{
				byte[] array = new byte[(this._buffer.Length * 2 > this._buffer.Length + count) ? (this._buffer.Length * 2) : (this._buffer.Length + count)];
				Buffer.BlockCopy(this._buffer, 0, array, 0, this._offset);
				this._buffer = array;
			}
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x000B586C File Offset: 0x000B3A6C
		internal void Append(byte value)
		{
			this.EnsureBuffer(1);
			byte[] buffer = this._buffer;
			int offset = this._offset;
			this._offset = offset + 1;
			buffer[offset] = value;
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000B5899 File Offset: 0x000B3A99
		internal void Append(byte[] value)
		{
			this.Append(value, 0, value.Length);
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000B58A6 File Offset: 0x000B3AA6
		internal void Append(byte[] value, int offset, int count)
		{
			this.EnsureBuffer(count);
			Buffer.BlockCopy(value, offset, this._buffer, this._offset, count);
			this._offset += count;
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x000B58D1 File Offset: 0x000B3AD1
		internal void Append(string value)
		{
			this.Append(value, false);
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x000B58DB File Offset: 0x000B3ADB
		internal void Append(string value, bool allowUnicode)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			this.Append(value, 0, value.Length, allowUnicode);
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000B58F8 File Offset: 0x000B3AF8
		internal void Append(string value, int offset, int count, bool allowUnicode)
		{
			if (allowUnicode)
			{
				int byteCount = Encoding.UTF8.GetByteCount(value, offset, count);
				this.EnsureBuffer(byteCount);
				Encoding.UTF8.GetBytes(value, offset, count, this._buffer, this._offset);
				this._offset += byteCount;
				return;
			}
			this.Append(value, offset, count);
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000B5950 File Offset: 0x000B3B50
		internal void Append(string value, int offset, int count)
		{
			this.EnsureBuffer(count);
			for (int i = 0; i < count; i++)
			{
				char c = value[offset + i];
				if (c > 'ÿ')
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", c));
				}
				this._buffer[this._offset + i] = (byte)c;
			}
			this._offset += count;
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06003284 RID: 12932 RVA: 0x000B59B7 File Offset: 0x000B3BB7
		internal int Length
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000B59BF File Offset: 0x000B3BBF
		internal byte[] GetBuffer()
		{
			return this._buffer;
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000B59C7 File Offset: 0x000B3BC7
		internal void Reset()
		{
			this._offset = 0;
		}

		// Token: 0x04001ED1 RID: 7889
		private byte[] _buffer;

		// Token: 0x04001ED2 RID: 7890
		private int _offset;
	}
}
