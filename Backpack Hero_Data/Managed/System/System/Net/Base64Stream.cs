using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net
{
	// Token: 0x02000382 RID: 898
	internal sealed class Base64Stream : DelegatedStream, IEncodableStream
	{
		// Token: 0x06001D73 RID: 7539 RVA: 0x0006B6F9 File Offset: 0x000698F9
		internal Base64Stream(Stream stream, Base64WriteStateInfo writeStateInfo)
			: base(stream)
		{
			this._writeState = new Base64WriteStateInfo();
			this._lineLength = writeStateInfo.MaxLineLength;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0006B719 File Offset: 0x00069919
		internal Base64Stream(Base64WriteStateInfo writeStateInfo)
			: base(new MemoryStream())
		{
			this._lineLength = writeStateInfo.MaxLineLength;
			this._writeState = writeStateInfo;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x0006B73C File Offset: 0x0006993C
		private Base64Stream.ReadStateInfo ReadState
		{
			get
			{
				Base64Stream.ReadStateInfo readStateInfo;
				if ((readStateInfo = this._readState) == null)
				{
					readStateInfo = (this._readState = new Base64Stream.ReadStateInfo());
				}
				return readStateInfo;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0006B761 File Offset: 0x00069961
		internal Base64WriteStateInfo WriteState
		{
			get
			{
				return this._writeState;
			}
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0006B76C File Offset: 0x0006996C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
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
			Base64Stream.ReadAsyncResult readAsyncResult = new Base64Stream.ReadAsyncResult(this, buffer, offset, count, callback, state);
			readAsyncResult.Read();
			return readAsyncResult;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0006B7C4 File Offset: 0x000699C4
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
			Base64Stream.WriteAsyncResult writeAsyncResult = new Base64Stream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0006B81C File Offset: 0x00069A1C
		public override void Close()
		{
			if (this._writeState != null && this.WriteState.Length > 0)
			{
				int padding = this.WriteState.Padding;
				if (padding != 1)
				{
					if (padding == 2)
					{
						this.WriteState.Append(new byte[]
						{
							Base64Stream.s_base64EncodeMap[(int)this.WriteState.LastBits],
							Base64Stream.s_base64EncodeMap[64],
							Base64Stream.s_base64EncodeMap[64]
						});
					}
				}
				else
				{
					this.WriteState.Append(new byte[]
					{
						Base64Stream.s_base64EncodeMap[(int)this.WriteState.LastBits],
						Base64Stream.s_base64EncodeMap[64]
					});
				}
				this.WriteState.Padding = 0;
				this.FlushInternal();
			}
			base.Close();
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0006B8E0 File Offset: 0x00069AE0
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
			while (ptr3 < ptr5)
			{
				if (*ptr3 == 13 || *ptr3 == 10 || *ptr3 == 61 || *ptr3 == 32 || *ptr3 == 9)
				{
					ptr3++;
				}
				else
				{
					byte b = Base64Stream.s_base64DecodeMap[(int)(*ptr3)];
					if (b == 255)
					{
						throw new FormatException("An invalid character was found in the Base-64 stream.");
					}
					switch (this.ReadState.Pos)
					{
					case 0:
					{
						this.ReadState.Val = (byte)(b << 2);
						Base64Stream.ReadStateInfo readState = this.ReadState;
						byte b2 = readState.Pos;
						readState.Pos = b2 + 1;
						break;
					}
					case 1:
					{
						*(ptr4++) = (byte)((int)this.ReadState.Val + (b >> 4));
						this.ReadState.Val = (byte)(b << 4);
						Base64Stream.ReadStateInfo readState2 = this.ReadState;
						byte b2 = readState2.Pos;
						readState2.Pos = b2 + 1;
						break;
					}
					case 2:
					{
						*(ptr4++) = (byte)((int)this.ReadState.Val + (b >> 2));
						this.ReadState.Val = (byte)(b << 6);
						Base64Stream.ReadStateInfo readState3 = this.ReadState;
						byte b2 = readState3.Pos;
						readState3.Pos = b2 + 1;
						break;
					}
					case 3:
						*(ptr4++) = this.ReadState.Val + b;
						this.ReadState.Pos = 0;
						break;
					}
					ptr3++;
				}
			}
			return (int)((long)(ptr4 - ptr2));
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0006BA70 File Offset: 0x00069C70
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			return this.EncodeBytes(buffer, offset, count, true, true);
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0006BA80 File Offset: 0x00069C80
		internal int EncodeBytes(byte[] buffer, int offset, int count, bool dontDeferFinalBytes, bool shouldAppendSpaceToCRLF)
		{
			this.WriteState.AppendHeader();
			int i = offset;
			int num = this.WriteState.Padding;
			if (num != 1)
			{
				if (num == 2)
				{
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)this.WriteState.LastBits | ((buffer[i] & 240) >> 4)]);
					if (count == 1)
					{
						this.WriteState.LastBits = (byte)((buffer[i] & 15) << 2);
						this.WriteState.Padding = 1;
						i++;
						return i - offset;
					}
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[((int)(buffer[i] & 15) << 2) | ((buffer[i + 1] & 192) >> 6)]);
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i + 1] & 63)]);
					i += 2;
					count -= 2;
					this.WriteState.Padding = 0;
				}
			}
			else
			{
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)this.WriteState.LastBits | ((buffer[i] & 192) >> 6)]);
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i] & 63)]);
				i++;
				count--;
				this.WriteState.Padding = 0;
			}
			int num2 = i + (count - count % 3);
			while (i < num2)
			{
				if (this._lineLength != -1 && this.WriteState.CurrentLineLength + 4 + this._writeState.FooterLength > this._lineLength)
				{
					this.WriteState.AppendCRLF(shouldAppendSpaceToCRLF);
				}
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(buffer[i] & 252) >> 2]);
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[((int)(buffer[i] & 3) << 4) | ((buffer[i + 1] & 240) >> 4)]);
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[((int)(buffer[i + 1] & 15) << 2) | ((buffer[i + 2] & 192) >> 6)]);
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i + 2] & 63)]);
				i += 3;
			}
			i = num2;
			if (count % 3 != 0 && this._lineLength != -1 && this.WriteState.CurrentLineLength + 4 + this._writeState.FooterLength >= this._lineLength)
			{
				this.WriteState.AppendCRLF(shouldAppendSpaceToCRLF);
			}
			num = count % 3;
			if (num != 1)
			{
				if (num == 2)
				{
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(buffer[i] & 252) >> 2]);
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[((int)(buffer[i] & 3) << 4) | ((buffer[i + 1] & 240) >> 4)]);
					if (dontDeferFinalBytes)
					{
						this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i + 1] & 15) << 2]);
						this.WriteState.Append(Base64Stream.s_base64EncodeMap[64]);
						this.WriteState.Padding = 0;
					}
					else
					{
						this.WriteState.LastBits = (byte)((buffer[i + 1] & 15) << 2);
						this.WriteState.Padding = 1;
					}
					i += 2;
				}
			}
			else
			{
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(buffer[i] & 252) >> 2]);
				if (dontDeferFinalBytes)
				{
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)((byte)((buffer[i] & 3) << 4))]);
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[64]);
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[64]);
					this.WriteState.Padding = 0;
				}
				else
				{
					this.WriteState.LastBits = (byte)((buffer[i] & 3) << 4);
					this.WriteState.Padding = 2;
				}
				i++;
			}
			this.WriteState.AppendFooter();
			return i - offset;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x00007575 File Offset: 0x00005775
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0006BE21 File Offset: 0x0006A021
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0006BE44 File Offset: 0x0006A044
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return Base64Stream.ReadAsyncResult.End(asyncResult);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0006BE5A File Offset: 0x0006A05A
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Base64Stream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0006BE70 File Offset: 0x0006A070
		public override void Flush()
		{
			if (this._writeState != null && this.WriteState.Length > 0)
			{
				this.FlushInternal();
			}
			base.Flush();
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0006BE94 File Offset: 0x0006A094
		private void FlushInternal()
		{
			base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
			this.WriteState.Reset();
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0006BEC0 File Offset: 0x0006A0C0
		public override int Read(byte[] buffer, int offset, int count)
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
			for (;;)
			{
				int num = base.Read(buffer, offset, count);
				if (num == 0)
				{
					break;
				}
				num = this.DecodeBytes(buffer, offset, num);
				if (num > 0)
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0006BF24 File Offset: 0x0006A124
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
				num += this.EncodeBytes(buffer, offset + num, count - num, false, false);
				if (num >= count)
				{
					break;
				}
				this.FlushInternal();
			}
		}

		// Token: 0x04000F4E RID: 3918
		private static readonly byte[] s_base64DecodeMap = new byte[]
		{
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, 62, byte.MaxValue, byte.MaxValue, byte.MaxValue, 63, 52, 53,
			54, 55, 56, 57, 58, 59, 60, 61, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 0, 1, 2, 3, 4,
			5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
			15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
			25, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 26, 27, 28,
			29, 30, 31, 32, 33, 34, 35, 36, 37, 38,
			39, 40, 41, 42, 43, 44, 45, 46, 47, 48,
			49, 50, 51, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
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

		// Token: 0x04000F4F RID: 3919
		private static readonly byte[] s_base64EncodeMap = new byte[]
		{
			65, 66, 67, 68, 69, 70, 71, 72, 73, 74,
			75, 76, 77, 78, 79, 80, 81, 82, 83, 84,
			85, 86, 87, 88, 89, 90, 97, 98, 99, 100,
			101, 102, 103, 104, 105, 106, 107, 108, 109, 110,
			111, 112, 113, 114, 115, 116, 117, 118, 119, 120,
			121, 122, 48, 49, 50, 51, 52, 53, 54, 55,
			56, 57, 43, 47, 61
		};

		// Token: 0x04000F50 RID: 3920
		private readonly int _lineLength;

		// Token: 0x04000F51 RID: 3921
		private readonly Base64WriteStateInfo _writeState;

		// Token: 0x04000F52 RID: 3922
		private Base64Stream.ReadStateInfo _readState;

		// Token: 0x04000F53 RID: 3923
		private const int SizeOfBase64EncodedChar = 4;

		// Token: 0x04000F54 RID: 3924
		private const byte InvalidBase64Value = 255;

		// Token: 0x02000383 RID: 899
		private sealed class ReadAsyncResult : LazyAsyncResult
		{
			// Token: 0x06001D86 RID: 7558 RVA: 0x0006BFBA File Offset: 0x0006A1BA
			internal ReadAsyncResult(Base64Stream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this._parent = parent;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
			}

			// Token: 0x06001D87 RID: 7559 RVA: 0x0006BFE4 File Offset: 0x0006A1E4
			private bool CompleteRead(IAsyncResult result)
			{
				this._read = this._parent.BaseStream.EndRead(result);
				if (this._read == 0)
				{
					base.InvokeCallback();
					return true;
				}
				this._read = this._parent.DecodeBytes(this._buffer, this._offset, this._read);
				if (this._read > 0)
				{
					base.InvokeCallback();
					return true;
				}
				return false;
			}

			// Token: 0x06001D88 RID: 7560 RVA: 0x0006C050 File Offset: 0x0006A250
			internal void Read()
			{
				IAsyncResult asyncResult;
				do
				{
					asyncResult = this._parent.BaseStream.BeginRead(this._buffer, this._offset, this._count, Base64Stream.ReadAsyncResult.s_onRead, this);
				}
				while (asyncResult.CompletedSynchronously && !this.CompleteRead(asyncResult));
			}

			// Token: 0x06001D89 RID: 7561 RVA: 0x0006C098 File Offset: 0x0006A298
			private static void OnRead(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Base64Stream.ReadAsyncResult readAsyncResult = (Base64Stream.ReadAsyncResult)result.AsyncState;
					try
					{
						if (!readAsyncResult.CompleteRead(result))
						{
							readAsyncResult.Read();
						}
					}
					catch (Exception ex)
					{
						if (readAsyncResult.IsCompleted)
						{
							throw;
						}
						readAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x06001D8A RID: 7562 RVA: 0x0006C0F0 File Offset: 0x0006A2F0
			internal static int End(IAsyncResult result)
			{
				Base64Stream.ReadAsyncResult readAsyncResult = (Base64Stream.ReadAsyncResult)result;
				readAsyncResult.InternalWaitForCompletion();
				return readAsyncResult._read;
			}

			// Token: 0x04000F55 RID: 3925
			private readonly Base64Stream _parent;

			// Token: 0x04000F56 RID: 3926
			private readonly byte[] _buffer;

			// Token: 0x04000F57 RID: 3927
			private readonly int _offset;

			// Token: 0x04000F58 RID: 3928
			private readonly int _count;

			// Token: 0x04000F59 RID: 3929
			private int _read;

			// Token: 0x04000F5A RID: 3930
			private static readonly AsyncCallback s_onRead = new AsyncCallback(Base64Stream.ReadAsyncResult.OnRead);
		}

		// Token: 0x02000384 RID: 900
		private sealed class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x06001D8C RID: 7564 RVA: 0x0006C117 File Offset: 0x0006A317
			internal WriteAsyncResult(Base64Stream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this._parent = parent;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
			}

			// Token: 0x06001D8D RID: 7565 RVA: 0x0006C144 File Offset: 0x0006A344
			internal void Write()
			{
				for (;;)
				{
					this._written += this._parent.EncodeBytes(this._buffer, this._offset + this._written, this._count - this._written, false, false);
					if (this._written >= this._count)
					{
						break;
					}
					IAsyncResult asyncResult = this._parent.BaseStream.BeginWrite(this._parent.WriteState.Buffer, 0, this._parent.WriteState.Length, Base64Stream.WriteAsyncResult.s_onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x06001D8E RID: 7566 RVA: 0x0006C1EB File Offset: 0x0006A3EB
			private void CompleteWrite(IAsyncResult result)
			{
				this._parent.BaseStream.EndWrite(result);
				this._parent.WriteState.Reset();
			}

			// Token: 0x06001D8F RID: 7567 RVA: 0x0006C210 File Offset: 0x0006A410
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Base64Stream.WriteAsyncResult writeAsyncResult = (Base64Stream.WriteAsyncResult)result.AsyncState;
					try
					{
						writeAsyncResult.CompleteWrite(result);
						writeAsyncResult.Write();
					}
					catch (Exception ex)
					{
						if (writeAsyncResult.IsCompleted)
						{
							throw;
						}
						writeAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x06001D90 RID: 7568 RVA: 0x0006C264 File Offset: 0x0006A464
			internal static void End(IAsyncResult result)
			{
				((Base64Stream.WriteAsyncResult)result).InternalWaitForCompletion();
			}

			// Token: 0x04000F5B RID: 3931
			private static readonly AsyncCallback s_onWrite = new AsyncCallback(Base64Stream.WriteAsyncResult.OnWrite);

			// Token: 0x04000F5C RID: 3932
			private readonly Base64Stream _parent;

			// Token: 0x04000F5D RID: 3933
			private readonly byte[] _buffer;

			// Token: 0x04000F5E RID: 3934
			private readonly int _offset;

			// Token: 0x04000F5F RID: 3935
			private readonly int _count;

			// Token: 0x04000F60 RID: 3936
			private int _written;
		}

		// Token: 0x02000385 RID: 901
		private sealed class ReadStateInfo
		{
			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x06001D92 RID: 7570 RVA: 0x0006C285 File Offset: 0x0006A485
			// (set) Token: 0x06001D93 RID: 7571 RVA: 0x0006C28D File Offset: 0x0006A48D
			internal byte Val { get; set; }

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x06001D94 RID: 7572 RVA: 0x0006C296 File Offset: 0x0006A496
			// (set) Token: 0x06001D95 RID: 7573 RVA: 0x0006C29E File Offset: 0x0006A49E
			internal byte Pos { get; set; }
		}
	}
}
