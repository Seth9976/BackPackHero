using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200061D RID: 1565
	internal class QuotedPrintableStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x06003230 RID: 12848 RVA: 0x000B4070 File Offset: 0x000B2270
		internal QuotedPrintableStream(Stream stream, int lineLength)
			: base(stream)
		{
			if (lineLength < 0)
			{
				throw new ArgumentOutOfRangeException("lineLength");
			}
			this._lineLength = lineLength;
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000B408F File Offset: 0x000B228F
		internal QuotedPrintableStream(Stream stream, bool encodeCRLF)
			: this(stream, 70)
		{
			this._encodeCRLF = encodeCRLF;
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x000B40A4 File Offset: 0x000B22A4
		private QuotedPrintableStream.ReadStateInfo ReadState
		{
			get
			{
				QuotedPrintableStream.ReadStateInfo readStateInfo;
				if ((readStateInfo = this._readState) == null)
				{
					readStateInfo = (this._readState = new QuotedPrintableStream.ReadStateInfo());
				}
				return readStateInfo;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000B40CC File Offset: 0x000B22CC
		internal WriteStateInfoBase WriteState
		{
			get
			{
				WriteStateInfoBase writeStateInfoBase;
				if ((writeStateInfoBase = this._writeState) == null)
				{
					writeStateInfoBase = (this._writeState = new WriteStateInfoBase(1024, null, null, this._lineLength));
				}
				return writeStateInfoBase;
			}
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000B4100 File Offset: 0x000B2300
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
			QuotedPrintableStream.WriteAsyncResult writeAsyncResult = new QuotedPrintableStream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x000B4156 File Offset: 0x000B2356
		public override void Close()
		{
			this.FlushInternal();
			base.Close();
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x000B4164 File Offset: 0x000B2364
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
						byte b = QuotedPrintableStream.s_hexDecodeMap[(int)(*ptr3)];
						byte b2 = QuotedPrintableStream.s_hexDecodeMap[(int)ptr3[1]];
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
						byte b3 = QuotedPrintableStream.s_hexDecodeMap[(int)this.ReadState.Byte];
						byte b4 = QuotedPrintableStream.s_hexDecodeMap[(int)(*ptr3)];
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
								byte b5 = QuotedPrintableStream.s_hexDecodeMap[(int)ptr3[1]];
								byte b6 = QuotedPrintableStream.s_hexDecodeMap[(int)ptr3[2]];
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
				*(ptr4++) = *(ptr3++);
			}
			return (int)((long)(ptr4 - ptr2));
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x000B43C8 File Offset: 0x000B25C8
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			int i;
			for (i = offset; i < count + offset; i++)
			{
				if ((this._lineLength != -1 && this.WriteState.CurrentLineLength + 3 + 2 >= this._lineLength && (buffer[i] == 32 || buffer[i] == 9 || buffer[i] == 13 || buffer[i] == 10)) || this._writeState.CurrentLineLength + 3 + 2 >= 70)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
					{
						return i - offset;
					}
					this.WriteState.Append(61);
					this.WriteState.AppendCRLF(false);
				}
				if (buffer[i] == 13 && i + 1 < count + offset && buffer[i + 1] == 10)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < (this._encodeCRLF ? 6 : 2))
					{
						return i - offset;
					}
					i++;
					if (this._encodeCRLF)
					{
						this.WriteState.Append(new byte[] { 61, 48, 68, 61, 48, 65 });
					}
					else
					{
						this.WriteState.AppendCRLF(false);
					}
				}
				else if ((buffer[i] < 32 && buffer[i] != 9) || buffer[i] == 61 || buffer[i] > 126)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
					{
						return i - offset;
					}
					this.WriteState.Append(61);
					this.WriteState.Append(QuotedPrintableStream.s_hexEncodeMap[buffer[i] >> 4]);
					this.WriteState.Append(QuotedPrintableStream.s_hexEncodeMap[(int)(buffer[i] & 15)]);
				}
				else
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 1)
					{
						return i - offset;
					}
					if ((buffer[i] == 9 || buffer[i] == 32) && i + 1 >= count + offset)
					{
						if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
						{
							return i - offset;
						}
						this.WriteState.Append(61);
						this.WriteState.Append(QuotedPrintableStream.s_hexEncodeMap[buffer[i] >> 4]);
						this.WriteState.Append(QuotedPrintableStream.s_hexEncodeMap[(int)(buffer[i] & 15)]);
					}
					else
					{
						this.WriteState.Append(buffer[i]);
					}
				}
			}
			return i - offset;
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x00007575 File Offset: 0x00005775
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000B4610 File Offset: 0x000B2810
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000B4633 File Offset: 0x000B2833
		public override void EndWrite(IAsyncResult asyncResult)
		{
			QuotedPrintableStream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000B463B File Offset: 0x000B283B
		public override void Flush()
		{
			this.FlushInternal();
			base.Flush();
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000B4649 File Offset: 0x000B2849
		private void FlushInternal()
		{
			if (this._writeState != null && this._writeState.Length > 0)
			{
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.BufferFlushed();
			}
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000B468C File Offset: 0x000B288C
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

		// Token: 0x04001EA2 RID: 7842
		private bool _encodeCRLF;

		// Token: 0x04001EA3 RID: 7843
		private const int SizeOfSoftCRLF = 3;

		// Token: 0x04001EA4 RID: 7844
		private const int SizeOfEncodedChar = 3;

		// Token: 0x04001EA5 RID: 7845
		private const int SizeOfEncodedCRLF = 6;

		// Token: 0x04001EA6 RID: 7846
		private const int SizeOfNonEncodedCRLF = 2;

		// Token: 0x04001EA7 RID: 7847
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

		// Token: 0x04001EA8 RID: 7848
		private static readonly byte[] s_hexEncodeMap = new byte[]
		{
			48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
			65, 66, 67, 68, 69, 70
		};

		// Token: 0x04001EA9 RID: 7849
		private int _lineLength;

		// Token: 0x04001EAA RID: 7850
		private QuotedPrintableStream.ReadStateInfo _readState;

		// Token: 0x04001EAB RID: 7851
		private WriteStateInfoBase _writeState;

		// Token: 0x0200061E RID: 1566
		private sealed class ReadStateInfo
		{
			// Token: 0x17000BA5 RID: 2981
			// (get) Token: 0x0600323F RID: 12863 RVA: 0x000B4720 File Offset: 0x000B2920
			// (set) Token: 0x06003240 RID: 12864 RVA: 0x000B4728 File Offset: 0x000B2928
			internal bool IsEscaped { get; set; }

			// Token: 0x17000BA6 RID: 2982
			// (get) Token: 0x06003241 RID: 12865 RVA: 0x000B4731 File Offset: 0x000B2931
			// (set) Token: 0x06003242 RID: 12866 RVA: 0x000B4739 File Offset: 0x000B2939
			internal short Byte { get; set; } = -1;
		}

		// Token: 0x0200061F RID: 1567
		private sealed class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x06003244 RID: 12868 RVA: 0x000B4751 File Offset: 0x000B2951
			internal WriteAsyncResult(QuotedPrintableStream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this._parent = parent;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
			}

			// Token: 0x06003245 RID: 12869 RVA: 0x000B477B File Offset: 0x000B297B
			private void CompleteWrite(IAsyncResult result)
			{
				this._parent.BaseStream.EndWrite(result);
				this._parent.WriteState.BufferFlushed();
			}

			// Token: 0x06003246 RID: 12870 RVA: 0x000B479E File Offset: 0x000B299E
			internal static void End(IAsyncResult result)
			{
				((QuotedPrintableStream.WriteAsyncResult)result).InternalWaitForCompletion();
			}

			// Token: 0x06003247 RID: 12871 RVA: 0x000B47AC File Offset: 0x000B29AC
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					QuotedPrintableStream.WriteAsyncResult writeAsyncResult = (QuotedPrintableStream.WriteAsyncResult)result.AsyncState;
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

			// Token: 0x06003248 RID: 12872 RVA: 0x000B47F8 File Offset: 0x000B29F8
			internal void Write()
			{
				for (;;)
				{
					this._written += this._parent.EncodeBytes(this._buffer, this._offset + this._written, this._count - this._written);
					if (this._written >= this._count)
					{
						break;
					}
					IAsyncResult asyncResult = this._parent.BaseStream.BeginWrite(this._parent.WriteState.Buffer, 0, this._parent.WriteState.Length, QuotedPrintableStream.WriteAsyncResult.s_onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x04001EAE RID: 7854
			private readonly QuotedPrintableStream _parent;

			// Token: 0x04001EAF RID: 7855
			private readonly byte[] _buffer;

			// Token: 0x04001EB0 RID: 7856
			private readonly int _offset;

			// Token: 0x04001EB1 RID: 7857
			private readonly int _count;

			// Token: 0x04001EB2 RID: 7858
			private static readonly AsyncCallback s_onWrite = new AsyncCallback(QuotedPrintableStream.WriteAsyncResult.OnWrite);

			// Token: 0x04001EB3 RID: 7859
			private int _written;
		}
	}
}
