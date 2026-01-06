using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000251 RID: 593
	internal sealed class SslOverTdsStream : Stream
	{
		// Token: 0x06001AFF RID: 6911 RVA: 0x00086C5C File Offset: 0x00084E5C
		public SslOverTdsStream(Stream stream)
		{
			this._stream = stream;
			this._encapsulate = true;
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00086C72 File Offset: 0x00084E72
		public void FinishHandshake()
		{
			this._encapsulate = false;
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00086C7C File Offset: 0x00084E7C
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.ReadInternal(buffer, offset, count, CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00086CA5 File Offset: 0x00084EA5
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.WriteInternal(buffer, offset, count, CancellationToken.None, false).Wait();
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00086CBB File Offset: 0x00084EBB
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken token)
		{
			return this.WriteInternal(buffer, offset, count, token, true);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00086CC9 File Offset: 0x00084EC9
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken token)
		{
			return this.ReadInternal(buffer, offset, count, token, true);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00086CD8 File Offset: 0x00084ED8
		private async Task<int> ReadInternal(byte[] buffer, int offset, int count, CancellationToken token, bool async)
		{
			int i = 0;
			byte[] packetData = new byte[(count < 8) ? 8 : count];
			int num2;
			if (this._encapsulate)
			{
				if (this._packetBytes == 0)
				{
					while (i < 8)
					{
						int num = i;
						if (async)
						{
							num2 = await this._stream.ReadAsync(packetData, i, 8 - i, token).ConfigureAwait(false);
						}
						else
						{
							num2 = this._stream.Read(packetData, i, 8 - i);
						}
						i = num + num2;
					}
					this._packetBytes = ((int)packetData[2] << 8) | (int)packetData[3];
					this._packetBytes -= 8;
				}
				if (count > this._packetBytes)
				{
					count = this._packetBytes;
				}
			}
			if (async)
			{
				num2 = await this._stream.ReadAsync(packetData, 0, count, token).ConfigureAwait(false);
			}
			else
			{
				num2 = this._stream.Read(packetData, 0, count);
			}
			i = num2;
			if (this._encapsulate)
			{
				this._packetBytes -= i;
			}
			Buffer.BlockCopy(packetData, 0, buffer, offset, i);
			return i;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00086D48 File Offset: 0x00084F48
		private async Task WriteInternal(byte[] buffer, int offset, int count, CancellationToken token, bool async)
		{
			int currentCount = 0;
			int currentOffset = offset;
			while (count > 0)
			{
				if (this._encapsulate)
				{
					if (count > 4088)
					{
						currentCount = 4088;
					}
					else
					{
						currentCount = count;
					}
					count -= currentCount;
					byte[] array = new byte[8 + currentCount];
					array[0] = 18;
					array[1] = ((count > 0) ? 0 : 1);
					array[2] = (byte)((currentCount + 8) / 256);
					array[3] = (byte)((currentCount + 8) % 256);
					array[4] = 0;
					array[5] = 0;
					array[6] = 0;
					array[7] = 0;
					for (int i = 8; i < array.Length; i++)
					{
						array[i] = buffer[currentOffset + (i - 8)];
					}
					if (async)
					{
						await this._stream.WriteAsync(array, 0, array.Length, token).ConfigureAwait(false);
					}
					else
					{
						this._stream.Write(array, 0, array.Length);
					}
				}
				else
				{
					currentCount = count;
					count = 0;
					if (async)
					{
						await this._stream.WriteAsync(buffer, currentOffset, currentCount, token).ConfigureAwait(false);
					}
					else
					{
						this._stream.Write(buffer, currentOffset, currentCount);
					}
				}
				if (async)
				{
					await this._stream.FlushAsync().ConfigureAwait(false);
				}
				else
				{
					this._stream.Flush();
				}
				currentOffset += currentCount;
			}
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0007C361 File Offset: 0x0007A561
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00086DB5 File Offset: 0x00084FB5
		public override void Flush()
		{
			if (!(this._stream is PipeStream))
			{
				this._stream.Flush();
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0007C361 File Offset: 0x0007A561
		// (set) Token: 0x06001B0A RID: 6922 RVA: 0x0007C361 File Offset: 0x0007A561
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0007C361 File Offset: 0x0007A561
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00086DCF File Offset: 0x00084FCF
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x00086DDC File Offset: 0x00084FDC
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x0007C361 File Offset: 0x0007A561
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0400136F RID: 4975
		private readonly Stream _stream;

		// Token: 0x04001370 RID: 4976
		private int _packetBytes;

		// Token: 0x04001371 RID: 4977
		private bool _encapsulate;

		// Token: 0x04001372 RID: 4978
		private const int PACKET_SIZE_WITHOUT_HEADER = 4088;

		// Token: 0x04001373 RID: 4979
		private const int PRELOGIN_PACKET_TYPE = 18;
	}
}
