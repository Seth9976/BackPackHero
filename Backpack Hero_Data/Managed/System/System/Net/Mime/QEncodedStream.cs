using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200061A RID: 1562
	internal class QEncodedStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x06003215 RID: 12821 RVA: 0x000B3943 File Offset: 0x000B1B43
		internal QEncodedStream(WriteStateInfoBase wsi)
			: base(new MemoryStream())
		{
			this._writeState = wsi;
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x000B3958 File Offset: 0x000B1B58
		private QEncodedStream.ReadStateInfo ReadState
		{
			get
			{
				QEncodedStream.ReadStateInfo readStateInfo;
				if ((readStateInfo = this._readState) == null)
				{
					readStateInfo = (this._readState = new QEncodedStream.ReadStateInfo());
				}
				return readStateInfo;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x000B397D File Offset: 0x000B1B7D
		internal WriteStateInfoBase WriteState
		{
			get
			{
				return this._writeState;
			}
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x000B3988 File Offset: 0x000B1B88
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			QEncodedStream.WriteAsyncResult writeAsyncResult = new QEncodedStream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000B39DE File Offset: 0x000B1BDE
		public override void Close()
		{
			this.FlushInternal();
			base.Close();
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000B39EC File Offset: 0x000B1BEC
		public unsafe int DecodeBytes(byte[] buffer, int offset, int count)
		{
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			byte* ptr2 = ptr + offset;
			byte* ptr3 = ptr2;
			byte* ptr4 = ptr2;
			byte* ptr5 = ptr2 + count;
			if (this.ReadState.IsEscaped)
			{
				if (this.ReadState.Byte == -1)
				{
					if (count == 1)
					{
						this.ReadState.Byte = (short)(*ptr3);
						return 0;
					}
					if (*ptr3 != 13 || ptr3[1] != 10)
					{
						byte b = QEncodedStream.s_hexDecodeMap[(int)(*ptr3)];
						byte b2 = QEncodedStream.s_hexDecodeMap[(int)ptr3[1]];
						if (b == 255)
						{
							throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b));
						}
						if (b2 == 255)
						{
							throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b2));
						}
						*(ptr4++) = (byte)(((int)b << 4) + (int)b2);
					}
					ptr3 += 2;
				}
				else
				{
					if (this.ReadState.Byte != 13 || *ptr3 != 10)
					{
						byte b3 = QEncodedStream.s_hexDecodeMap[(int)this.ReadState.Byte];
						byte b4 = QEncodedStream.s_hexDecodeMap[(int)(*ptr3)];
						if (b3 == 255)
						{
							throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b3));
						}
						if (b4 == 255)
						{
							throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b4));
						}
						*(ptr4++) = (byte)(((int)b3 << 4) + (int)b4);
					}
					ptr3++;
				}
				this.ReadState.IsEscaped = false;
				this.ReadState.Byte = -1;
			}
			while (ptr3 < ptr5)
			{
				if (*ptr3 == 61)
				{
					long num = (long)(ptr5 - ptr3);
					if (num != 1L)
					{
						if (num != 2L)
						{
							if (ptr3[1] != 13 || ptr3[2] != 10)
							{
								byte b5 = QEncodedStream.s_hexDecodeMap[(int)ptr3[1]];
								byte b6 = QEncodedStream.s_hexDecodeMap[(int)ptr3[2]];
								if (b5 == 255)
								{
									throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b5));
								}
								if (b6 == 255)
								{
									throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b6));
								}
								*(ptr4++) = (byte)(((int)b5 << 4) + (int)b6);
							}
							ptr3 += 3;
							continue;
						}
						this.ReadState.Byte = (short)ptr3[1];
					}
					this.ReadState.IsEscaped = true;
					break;
				}
				if (*ptr3 == 95)
				{
					*(ptr4++) = 32;
					ptr3++;
				}
				else
				{
					*(ptr4++) = *(ptr3++);
				}
			}
			return (int)((long)(ptr4 - ptr2));
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000B3C68 File Offset: 0x000B1E68
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			this._writeState.AppendHeader();
			int i;
			for (i = offset; i < count + offset; i++)
			{
				if ((this.WriteState.CurrentLineLength + 3 + this.WriteState.FooterLength >= this.WriteState.MaxLineLength && (buffer[i] == 32 || buffer[i] == 9 || buffer[i] == 13 || buffer[i] == 10)) || this.WriteState.CurrentLineLength + this._writeState.FooterLength >= this.WriteState.MaxLineLength)
				{
					this.WriteState.AppendCRLF(true);
				}
				if (buffer[i] == 13 && i + 1 < count + offset && buffer[i + 1] == 10)
				{
					i++;
					this.WriteState.Append(new byte[] { 61, 48, 68, 61, 48, 65 });
				}
				else if (buffer[i] == 32)
				{
					this.WriteState.Append(95);
				}
				else if (QEncodedStream.IsAsciiLetterOrDigit((char)buffer[i]))
				{
					this.WriteState.Append(buffer[i]);
				}
				else
				{
					this.WriteState.Append(61);
					this.WriteState.Append(QEncodedStream.s_hexEncodeMap[buffer[i] >> 4]);
					this.WriteState.Append(QEncodedStream.s_hexEncodeMap[(int)(buffer[i] & 15)]);
				}
			}
			this.WriteState.AppendFooter();
			return i - offset;
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000B3DB8 File Offset: 0x000B1FB8
		private static bool IsAsciiLetterOrDigit(char character)
		{
			return QEncodedStream.IsAsciiLetter(character) || (character >= '0' && character <= '9');
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x00027F10 File Offset: 0x00026110
		private static bool IsAsciiLetter(char character)
		{
			return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z');
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x00007575 File Offset: 0x00005775
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000B3DD3 File Offset: 0x000B1FD3
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000B3DF6 File Offset: 0x000B1FF6
		public override void EndWrite(IAsyncResult asyncResult)
		{
			QEncodedStream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000B3DFE File Offset: 0x000B1FFE
		public override void Flush()
		{
			this.FlushInternal();
			base.Flush();
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000B3E0C File Offset: 0x000B200C
		private void FlushInternal()
		{
			if (this._writeState != null && this._writeState.Length > 0)
			{
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.Reset();
			}
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000B3E4C File Offset: 0x000B204C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = 0;
			for (;;)
			{
				num += this.EncodeBytes(buffer, offset + num, count - num);
				if (num >= count)
				{
					break;
				}
				this.FlushInternal();
			}
		}

		// Token: 0x04001E95 RID: 7829
		private const int SizeOfFoldingCRLF = 3;

		// Token: 0x04001E96 RID: 7830
		private static readonly byte[] s_hexDecodeMap = new byte[]
		{
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 0, 1,
			2, 3, 4, 5, 6, 7, 8, 9, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 10, 11, 12, 13, 14,
			15, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 10, 11, 12,
			13, 14, 15, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue
		};

		// Token: 0x04001E97 RID: 7831
		private static readonly byte[] s_hexEncodeMap = new byte[]
		{
			48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
			65, 66, 67, 68, 69, 70
		};

		// Token: 0x04001E98 RID: 7832
		private QEncodedStream.ReadStateInfo _readState;

		// Token: 0x04001E99 RID: 7833
		private WriteStateInfoBase _writeState;

		// Token: 0x0200061B RID: 1563
		private sealed class ReadStateInfo
		{
			// Token: 0x17000BA1 RID: 2977
			// (get) Token: 0x06003225 RID: 12837 RVA: 0x000B3EE0 File Offset: 0x000B20E0
			// (set) Token: 0x06003226 RID: 12838 RVA: 0x000B3EE8 File Offset: 0x000B20E8
			internal bool IsEscaped { get; set; }

			// Token: 0x17000BA2 RID: 2978
			// (get) Token: 0x06003227 RID: 12839 RVA: 0x000B3EF1 File Offset: 0x000B20F1
			// (set) Token: 0x06003228 RID: 12840 RVA: 0x000B3EF9 File Offset: 0x000B20F9
			internal short Byte { get; set; } = -1;
		}

		// Token: 0x0200061C RID: 1564
		private class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x0600322A RID: 12842 RVA: 0x000B3F11 File Offset: 0x000B2111
			internal WriteAsyncResult(QEncodedStream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this._parent = parent;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
			}

			// Token: 0x0600322B RID: 12843 RVA: 0x000B3F3B File Offset: 0x000B213B
			private void CompleteWrite(IAsyncResult result)
			{
				this._parent.BaseStream.EndWrite(result);
				this._parent.WriteState.Reset();
			}

			// Token: 0x0600322C RID: 12844 RVA: 0x000B3F5E File Offset: 0x000B215E
			internal static void End(IAsyncResult result)
			{
				((QEncodedStream.WriteAsyncResult)result).InternalWaitForCompletion();
			}

			// Token: 0x0600322D RID: 12845 RVA: 0x000B3F6C File Offset: 0x000B216C
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					QEncodedStream.WriteAsyncResult writeAsyncResult = (QEncodedStream.WriteAsyncResult)result.AsyncState;
					try
					{
						writeAsyncResult.CompleteWrite(result);
						writeAsyncResult.Write();
					}
					catch (Exception ex)
					{
						writeAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x0600322E RID: 12846 RVA: 0x000B3FB8 File Offset: 0x000B21B8
			internal void Write()
			{
				for (;;)
				{
					this._written += this._parent.EncodeBytes(this._buffer, this._offset + this._written, this._count - this._written);
					if (this._written >= this._count)
					{
						break;
					}
					IAsyncResult asyncResult = this._parent.BaseStream.BeginWrite(this._parent.WriteState.Buffer, 0, this._parent.WriteState.Length, QEncodedStream.WriteAsyncResult.s_onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x04001E9C RID: 7836
			private static readonly AsyncCallback s_onWrite = new AsyncCallback(QEncodedStream.WriteAsyncResult.OnWrite);

			// Token: 0x04001E9D RID: 7837
			private readonly QEncodedStream _parent;

			// Token: 0x04001E9E RID: 7838
			private readonly byte[] _buffer;

			// Token: 0x04001E9F RID: 7839
			private readonly int _offset;

			// Token: 0x04001EA0 RID: 7840
			private readonly int _count;

			// Token: 0x04001EA1 RID: 7841
			private int _written;
		}
	}
}
