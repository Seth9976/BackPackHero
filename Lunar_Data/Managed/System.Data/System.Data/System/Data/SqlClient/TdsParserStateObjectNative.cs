using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x02000229 RID: 553
	internal class TdsParserStateObjectNative : TdsParserStateObject
	{
		// Token: 0x060019B5 RID: 6581 RVA: 0x0008182B File Offset: 0x0007FA2B
		public TdsParserStateObjectNative(TdsParser parser)
			: base(parser)
		{
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0008184A File Offset: 0x0007FA4A
		internal TdsParserStateObjectNative(TdsParser parser, TdsParserStateObject physicalConnection, bool async)
			: base(parser, physicalConnection, async)
		{
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x0008186B File Offset: 0x0007FA6B
		internal SNIHandle Handle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00081873 File Offset: 0x0007FA73
		internal override uint Status
		{
			get
			{
				if (this._sessionHandle == null)
				{
					return uint.MaxValue;
				}
				return this._sessionHandle.Status;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x0008186B File Offset: 0x0007FA6B
		internal override object SessionHandle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x0008188A File Offset: 0x0007FA8A
		protected override object EmptyReadPacket
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00081898 File Offset: 0x0007FA98
		protected override void CreateSessionHandle(TdsParserStateObject physicalConnection, bool async)
		{
			TdsParserStateObjectNative tdsParserStateObjectNative = physicalConnection as TdsParserStateObjectNative;
			SNINativeMethodWrapper.ConsumerInfo consumerInfo = this.CreateConsumerInfo(async);
			this._sessionHandle = new SNIHandle(consumerInfo, tdsParserStateObjectNative.Handle);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000818C8 File Offset: 0x0007FAC8
		private SNINativeMethodWrapper.ConsumerInfo CreateConsumerInfo(bool async)
		{
			SNINativeMethodWrapper.ConsumerInfo consumerInfo = default(SNINativeMethodWrapper.ConsumerInfo);
			consumerInfo.defaultBufferSize = this._outBuff.Length;
			if (async)
			{
				consumerInfo.readDelegate = SNILoadHandle.SingletonInstance.ReadAsyncCallbackDispatcher;
				consumerInfo.writeDelegate = SNILoadHandle.SingletonInstance.WriteAsyncCallbackDispatcher;
				this._gcHandle = GCHandle.Alloc(this, GCHandleType.Normal);
				consumerInfo.key = (IntPtr)this._gcHandle;
			}
			return consumerInfo;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00081934 File Offset: 0x0007FB34
		internal override void CreatePhysicalSNIHandle(string serverName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, ref byte[] spnBuffer, bool flushCache, bool async, bool fParallel, bool isIntegratedSecurity)
		{
			spnBuffer = null;
			if (isIntegratedSecurity)
			{
				spnBuffer = new byte[SNINativeMethodWrapper.SniMaxComposedSpnLength];
			}
			SNINativeMethodWrapper.ConsumerInfo consumerInfo = this.CreateConsumerInfo(async);
			long num;
			if (9223372036854775807L == timerExpire)
			{
				num = 2147483647L;
			}
			else
			{
				num = ADP.TimerRemainingMilliseconds(timerExpire);
				if (num > 2147483647L)
				{
					num = 2147483647L;
				}
				else if (0L > num)
				{
					num = 0L;
				}
			}
			this._sessionHandle = new SNIHandle(consumerInfo, serverName, spnBuffer, ignoreSniOpenTimeout, checked((int)num), out instanceName, flushCache, !async, fParallel);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000819B3 File Offset: 0x0007FBB3
		protected override uint SNIPacketGetData(object packet, byte[] _inBuff, ref uint dataSize)
		{
			return SNINativeMethodWrapper.SNIPacketGetData((IntPtr)packet, _inBuff, ref dataSize);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000819C4 File Offset: 0x0007FBC4
		protected override bool CheckPacket(object packet, TaskCompletionSource<object> source)
		{
			IntPtr intPtr = (IntPtr)packet;
			return IntPtr.Zero == intPtr || (IntPtr.Zero != intPtr && source != null);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000819FA File Offset: 0x0007FBFA
		public void ReadAsyncCallback(IntPtr key, IntPtr packet, uint error)
		{
			this.ReadAsyncCallback(key, packet, error);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00081A05 File Offset: 0x0007FC05
		public void WriteAsyncCallback(IntPtr key, IntPtr packet, uint sniError)
		{
			this.WriteAsyncCallback(key, packet, sniError);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00081A10 File Offset: 0x0007FC10
		protected override void RemovePacketFromPendingList(object ptr)
		{
			IntPtr intPtr = (IntPtr)ptr;
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				SNIPacket snipacket;
				if (this._pendingWritePackets.TryGetValue(intPtr, out snipacket))
				{
					this._pendingWritePackets.Remove(intPtr);
					this._writePacketCache.Add(snipacket);
				}
			}
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00081A7C File Offset: 0x0007FC7C
		internal override void Dispose()
		{
			SafeHandle sniPacket = this._sniPacket;
			SafeHandle sessionHandle = this._sessionHandle;
			SafeHandle sniAsyncAttnPacket = this._sniAsyncAttnPacket;
			this._sniPacket = null;
			this._sessionHandle = null;
			this._sniAsyncAttnPacket = null;
			base.DisposeCounters();
			if (sessionHandle != null || sniPacket != null)
			{
				if (sniPacket != null)
				{
					sniPacket.Dispose();
				}
				if (sniAsyncAttnPacket != null)
				{
					sniAsyncAttnPacket.Dispose();
				}
				if (sessionHandle != null)
				{
					sessionHandle.Dispose();
					base.DecrementPendingCallbacks(true);
				}
			}
			this.DisposePacketCache();
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00081AE8 File Offset: 0x0007FCE8
		protected override void FreeGcHandle(int remaining, bool release)
		{
			if ((remaining == 0 || release) && this._gcHandle.IsAllocated)
			{
				this._gcHandle.Free();
			}
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00081B0A File Offset: 0x0007FD0A
		internal override bool IsFailedHandle()
		{
			return this._sessionHandle.Status > 0U;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00081B1C File Offset: 0x0007FD1C
		internal override object ReadSyncOverAsync(int timeoutRemaining, out uint error)
		{
			SNIHandle handle = this.Handle;
			if (handle == null)
			{
				throw ADP.ClosedConnectionError();
			}
			IntPtr zero = IntPtr.Zero;
			error = SNINativeMethodWrapper.SNIReadSyncOverAsync(handle, ref zero, base.GetTimeoutRemaining());
			return zero;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00081B55 File Offset: 0x0007FD55
		internal override bool IsPacketEmpty(object readPacket)
		{
			return IntPtr.Zero == (IntPtr)readPacket;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00081B67 File Offset: 0x0007FD67
		internal override void ReleasePacket(object syncReadPacket)
		{
			SNINativeMethodWrapper.SNIPacketRelease((IntPtr)syncReadPacket);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00081B74 File Offset: 0x0007FD74
		internal override uint CheckConnection()
		{
			SNIHandle handle = this.Handle;
			if (handle != null)
			{
				return SNINativeMethodWrapper.SNICheckConnection(handle);
			}
			return 0U;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00081B94 File Offset: 0x0007FD94
		internal override object ReadAsync(out uint error, ref object handle)
		{
			IntPtr zero = IntPtr.Zero;
			error = SNINativeMethodWrapper.SNIReadAsync((SNIHandle)handle, ref zero);
			return zero;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00081BC0 File Offset: 0x0007FDC0
		internal override object CreateAndSetAttentionPacket()
		{
			SNIPacket snipacket = new SNIPacket(this.Handle);
			this._sniAsyncAttnPacket = snipacket;
			this.SetPacketData(snipacket, SQL.AttentionHeader, 8);
			return snipacket;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00081BEE File Offset: 0x0007FDEE
		internal override uint WritePacket(object packet, bool sync)
		{
			return SNINativeMethodWrapper.SNIWritePacket(this.Handle, (SNIPacket)packet, sync);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00081C04 File Offset: 0x0007FE04
		internal override object AddPacketToPendingList(object packetToAdd)
		{
			SNIPacket snipacket = (SNIPacket)packetToAdd;
			this._sniPacket = null;
			IntPtr intPtr = snipacket.DangerousGetHandle();
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._pendingWritePackets.Add(intPtr, snipacket);
			}
			return intPtr;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00081C68 File Offset: 0x0007FE68
		internal override bool IsValidPacket(object packetPointer)
		{
			return (IntPtr)packetPointer != IntPtr.Zero;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00081C7C File Offset: 0x0007FE7C
		internal override object GetResetWritePacket()
		{
			if (this._sniPacket != null)
			{
				SNINativeMethodWrapper.SNIPacketReset(this.Handle, SNINativeMethodWrapper.IOType.WRITE, this._sniPacket, SNINativeMethodWrapper.ConsumerNumber.SNI_Consumer_SNI);
			}
			else
			{
				object writePacketLockObject = this._writePacketLockObject;
				lock (writePacketLockObject)
				{
					this._sniPacket = this._writePacketCache.Take(this.Handle);
				}
			}
			return this._sniPacket;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00081CF0 File Offset: 0x0007FEF0
		internal override void ClearAllWritePackets()
		{
			if (this._sniPacket != null)
			{
				this._sniPacket.Dispose();
				this._sniPacket = null;
			}
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._writePacketCache.Clear();
			}
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00081D50 File Offset: 0x0007FF50
		internal override void SetPacketData(object packet, byte[] buffer, int bytesUsed)
		{
			SNINativeMethodWrapper.SNIPacketSetData((SNIPacket)packet, buffer, bytesUsed);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00081D5F File Offset: 0x0007FF5F
		internal override uint SniGetConnectionId(ref Guid clientConnectionId)
		{
			return SNINativeMethodWrapper.SniGetConnectionId(this.Handle, ref clientConnectionId);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00081D6D File Offset: 0x0007FF6D
		internal override uint DisabeSsl()
		{
			return SNINativeMethodWrapper.SNIRemoveProvider(this.Handle, SNINativeMethodWrapper.ProviderEnum.SSL_PROV);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00081D7B File Offset: 0x0007FF7B
		internal override uint EnableMars(ref uint info)
		{
			return SNINativeMethodWrapper.SNIAddProvider(this.Handle, SNINativeMethodWrapper.ProviderEnum.SMUX_PROV, ref info);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00081D8A File Offset: 0x0007FF8A
		internal override uint EnableSsl(ref uint info)
		{
			return SNINativeMethodWrapper.SNIAddProvider(this.Handle, SNINativeMethodWrapper.ProviderEnum.SSL_PROV, ref info);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00081D99 File Offset: 0x0007FF99
		internal override uint SetConnectionBufferSize(ref uint unsignedPacketSize)
		{
			return SNINativeMethodWrapper.SNISetInfo(this.Handle, SNINativeMethodWrapper.QTypes.SNI_QUERY_CONN_BUFSIZE, ref unsignedPacketSize);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00081DA8 File Offset: 0x0007FFA8
		internal override uint GenerateSspiClientContext(byte[] receivedBuff, uint receivedLength, ref byte[] sendBuff, ref uint sendLength, byte[] _sniSpnBuffer)
		{
			return SNINativeMethodWrapper.SNISecGenClientContext(this.Handle, receivedBuff, receivedLength, sendBuff, ref sendLength, _sniSpnBuffer);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x00081DBD File Offset: 0x0007FFBD
		internal override uint WaitForSSLHandShakeToComplete()
		{
			return SNINativeMethodWrapper.SNIWaitForSSLHandshakeToComplete(this.Handle, base.GetTimeoutRemaining());
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00081DD0 File Offset: 0x0007FFD0
		internal override void DisposePacketCache()
		{
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._writePacketCache.Dispose();
			}
		}

		// Token: 0x04001287 RID: 4743
		private SNIHandle _sessionHandle;

		// Token: 0x04001288 RID: 4744
		private SNIPacket _sniPacket;

		// Token: 0x04001289 RID: 4745
		internal SNIPacket _sniAsyncAttnPacket;

		// Token: 0x0400128A RID: 4746
		private readonly TdsParserStateObjectNative.WritePacketCache _writePacketCache = new TdsParserStateObjectNative.WritePacketCache();

		// Token: 0x0400128B RID: 4747
		private GCHandle _gcHandle;

		// Token: 0x0400128C RID: 4748
		private Dictionary<IntPtr, SNIPacket> _pendingWritePackets = new Dictionary<IntPtr, SNIPacket>();

		// Token: 0x0200022A RID: 554
		internal sealed class WritePacketCache : IDisposable
		{
			// Token: 0x060019DA RID: 6618 RVA: 0x00081E18 File Offset: 0x00080018
			public WritePacketCache()
			{
				this._disposed = false;
				this._packets = new Stack<SNIPacket>();
			}

			// Token: 0x060019DB RID: 6619 RVA: 0x00081E34 File Offset: 0x00080034
			public SNIPacket Take(SNIHandle sniHandle)
			{
				SNIPacket snipacket;
				if (this._packets.Count > 0)
				{
					snipacket = this._packets.Pop();
					SNINativeMethodWrapper.SNIPacketReset(sniHandle, SNINativeMethodWrapper.IOType.WRITE, snipacket, SNINativeMethodWrapper.ConsumerNumber.SNI_Consumer_SNI);
				}
				else
				{
					snipacket = new SNIPacket(sniHandle);
				}
				return snipacket;
			}

			// Token: 0x060019DC RID: 6620 RVA: 0x00081E6E File Offset: 0x0008006E
			public void Add(SNIPacket packet)
			{
				if (!this._disposed)
				{
					this._packets.Push(packet);
					return;
				}
				packet.Dispose();
			}

			// Token: 0x060019DD RID: 6621 RVA: 0x00081E8B File Offset: 0x0008008B
			public void Clear()
			{
				while (this._packets.Count > 0)
				{
					this._packets.Pop().Dispose();
				}
			}

			// Token: 0x060019DE RID: 6622 RVA: 0x00081EAD File Offset: 0x000800AD
			public void Dispose()
			{
				if (!this._disposed)
				{
					this._disposed = true;
					this.Clear();
				}
			}

			// Token: 0x0400128D RID: 4749
			private bool _disposed;

			// Token: 0x0400128E RID: 4750
			private Stack<SNIPacket> _packets;
		}
	}
}
