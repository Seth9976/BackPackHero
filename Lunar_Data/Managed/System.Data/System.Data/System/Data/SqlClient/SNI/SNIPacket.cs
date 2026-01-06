using System;
using System.Buffers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000246 RID: 582
	internal class SNIPacket : IDisposable, IEquatable<SNIPacket>
	{
		// Token: 0x06001A91 RID: 6801 RVA: 0x00084D14 File Offset: 0x00082F14
		public SNIPacket()
		{
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00084D27 File Offset: 0x00082F27
		public SNIPacket(int capacity)
		{
			this.Allocate(capacity);
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x00084D41 File Offset: 0x00082F41
		// (set) Token: 0x06001A94 RID: 6804 RVA: 0x00084D49 File Offset: 0x00082F49
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x00084D52 File Offset: 0x00082F52
		public int DataLeft
		{
			get
			{
				return this._length - this._offset;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x00084D61 File Offset: 0x00082F61
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x00084D69 File Offset: 0x00082F69
		public bool IsInvalid
		{
			get
			{
				return this._data == null;
			}
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00084D74 File Offset: 0x00082F74
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x00084D7C File Offset: 0x00082F7C
		public void SetCompletionCallback(SNIAsyncCallback completionCallback)
		{
			this._completionCallback = completionCallback;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x00084D85 File Offset: 0x00082F85
		public void InvokeCompletionCallback(uint sniErrorCode)
		{
			this._completionCallback(this, sniErrorCode);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00084D94 File Offset: 0x00082F94
		public void Allocate(int capacity)
		{
			if (this._data != null && this._data.Length < capacity)
			{
				if (this._isBufferFromArrayPool)
				{
					this._arrayPool.Return(this._data, false);
				}
				this._data = null;
			}
			if (this._data == null)
			{
				this._data = this._arrayPool.Rent(capacity);
				this._isBufferFromArrayPool = true;
			}
			this._capacity = capacity;
			this._length = 0;
			this._offset = 0;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00084E0C File Offset: 0x0008300C
		public SNIPacket Clone()
		{
			SNIPacket snipacket = new SNIPacket(this._capacity);
			Buffer.BlockCopy(this._data, 0, snipacket._data, 0, this._capacity);
			snipacket._length = this._length;
			snipacket._description = this._description;
			snipacket._completionCallback = this._completionCallback;
			return snipacket;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00084E63 File Offset: 0x00083063
		public void GetData(byte[] buffer, ref int dataSize)
		{
			Buffer.BlockCopy(this._data, 0, buffer, 0, this._length);
			dataSize = this._length;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00084E81 File Offset: 0x00083081
		public void SetData(byte[] data, int length)
		{
			this._data = data;
			this._length = length;
			this._capacity = data.Length;
			this._offset = 0;
			this._isBufferFromArrayPool = false;
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00084EA8 File Offset: 0x000830A8
		public int TakeData(SNIPacket packet, int size)
		{
			int num = this.TakeData(packet._data, packet._length, size);
			packet._length += num;
			return num;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00084ED8 File Offset: 0x000830D8
		public void AppendData(byte[] data, int size)
		{
			Buffer.BlockCopy(data, 0, this._data, this._length, size);
			this._length += size;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00084EFC File Offset: 0x000830FC
		public void AppendPacket(SNIPacket packet)
		{
			Buffer.BlockCopy(packet._data, 0, this._data, this._length, packet._length);
			this._length += packet._length;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00084F30 File Offset: 0x00083130
		public int TakeData(byte[] buffer, int dataOffset, int size)
		{
			if (this._offset >= this._length)
			{
				return 0;
			}
			if (this._offset + size > this._length)
			{
				size = this._length - this._offset;
			}
			Buffer.BlockCopy(this._data, this._offset, buffer, dataOffset, size);
			this._offset += size;
			return size;
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00084F8F File Offset: 0x0008318F
		public void Release()
		{
			if (this._data != null)
			{
				if (this._isBufferFromArrayPool)
				{
					this._arrayPool.Return(this._data, false);
				}
				this._data = null;
				this._capacity = 0;
			}
			this.Reset();
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00084FC7 File Offset: 0x000831C7
		public void Reset()
		{
			this._length = 0;
			this._offset = 0;
			this._description = null;
			this._completionCallback = null;
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00084FE8 File Offset: 0x000831E8
		public void ReadFromStreamAsync(Stream stream, SNIAsyncCallback callback)
		{
			bool error = false;
			stream.ReadAsync(this._data, 0, this._capacity, CancellationToken.None).ContinueWith(delegate(Task<int> t)
			{
				AggregateException exception = t.Exception;
				Exception ex = ((exception != null) ? exception.InnerException : null);
				if (ex != null)
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 35U, ex);
					error = true;
				}
				else
				{
					this._length = t.Result;
					if (this._length == 0)
					{
						SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 0U, 2U, string.Empty);
						error = true;
					}
				}
				if (error)
				{
					this.Release();
				}
				callback(this, error ? 1U : 0U);
			}, CancellationToken.None, TaskContinuationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00085045 File Offset: 0x00083245
		public void ReadFromStream(Stream stream)
		{
			this._length = stream.Read(this._data, 0, this._capacity);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00085060 File Offset: 0x00083260
		public void WriteToStream(Stream stream)
		{
			stream.Write(this._data, 0, this._length);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00085078 File Offset: 0x00083278
		public async void WriteToStreamAsync(Stream stream, SNIAsyncCallback callback, SNIProviders provider, bool disposeAfterWriteAsync = false)
		{
			uint status = 0U;
			try
			{
				await stream.WriteAsync(this._data, 0, this._length, CancellationToken.None).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				SNILoadHandle.SingletonInstance.LastError = new SNIError(provider, 35U, ex);
				status = 1U;
			}
			callback(this, status);
			if (disposeAfterWriteAsync)
			{
				this.Dispose();
			}
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00032F3A File Offset: 0x0003113A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x000850D0 File Offset: 0x000832D0
		public override bool Equals(object obj)
		{
			SNIPacket snipacket = obj as SNIPacket;
			return snipacket != null && this.Equals(snipacket);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000850F0 File Offset: 0x000832F0
		public bool Equals(SNIPacket packet)
		{
			return packet != null && packet == this;
		}

		// Token: 0x04001321 RID: 4897
		private byte[] _data;

		// Token: 0x04001322 RID: 4898
		private int _length;

		// Token: 0x04001323 RID: 4899
		private int _capacity;

		// Token: 0x04001324 RID: 4900
		private int _offset;

		// Token: 0x04001325 RID: 4901
		private string _description;

		// Token: 0x04001326 RID: 4902
		private SNIAsyncCallback _completionCallback;

		// Token: 0x04001327 RID: 4903
		private ArrayPool<byte> _arrayPool = ArrayPool<byte>.Shared;

		// Token: 0x04001328 RID: 4904
		private bool _isBufferFromArrayPool;
	}
}
