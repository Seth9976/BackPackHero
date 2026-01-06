using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000255 RID: 597
	internal class TdsParserStateObjectManaged : TdsParserStateObject
	{
		// Token: 0x06001B1B RID: 6939 RVA: 0x00087419 File Offset: 0x00085619
		public TdsParserStateObjectManaged(TdsParser parser)
			: base(parser)
		{
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00087443 File Offset: 0x00085643
		internal TdsParserStateObjectManaged(TdsParser parser, TdsParserStateObject physicalConnection, bool async)
			: base(parser, physicalConnection, async)
		{
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0008746F File Offset: 0x0008566F
		internal SNIHandle Handle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x00087477 File Offset: 0x00085677
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

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0008746F File Offset: 0x0008566F
		internal override object SessionHandle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x00003DF6 File Offset: 0x00001FF6
		protected override object EmptyReadPacket
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00087490 File Offset: 0x00085690
		protected override bool CheckPacket(object packet, TaskCompletionSource<object> source)
		{
			SNIPacket snipacket = packet as SNIPacket;
			return snipacket.IsInvalid || (!snipacket.IsInvalid && source != null);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000874BC File Offset: 0x000856BC
		protected override void CreateSessionHandle(TdsParserStateObject physicalConnection, bool async)
		{
			TdsParserStateObjectManaged tdsParserStateObjectManaged = physicalConnection as TdsParserStateObjectManaged;
			this._sessionHandle = tdsParserStateObjectManaged.CreateMarsSession(this, async);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000874DE File Offset: 0x000856DE
		internal SNIMarsHandle CreateMarsSession(object callbackObject, bool async)
		{
			return this._marsConnection.CreateMarsSession(callbackObject, async);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000874ED File Offset: 0x000856ED
		protected override uint SNIPacketGetData(object packet, byte[] _inBuff, ref uint dataSize)
		{
			return SNIProxy.Singleton.PacketGetData(packet as SNIPacket, _inBuff, ref dataSize);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00087504 File Offset: 0x00085704
		internal override void CreatePhysicalSNIHandle(string serverName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, ref byte[] spnBuffer, bool flushCache, bool async, bool parallel, bool isIntegratedSecurity)
		{
			this._sessionHandle = SNIProxy.Singleton.CreateConnectionHandle(this, serverName, ignoreSniOpenTimeout, timerExpire, out instanceName, ref spnBuffer, flushCache, async, parallel, isIntegratedSecurity);
			if (this._sessionHandle == null)
			{
				this._parser.ProcessSNIError(this);
				return;
			}
			if (async)
			{
				SNIAsyncCallback sniasyncCallback = new SNIAsyncCallback(this.ReadAsyncCallback);
				SNIAsyncCallback sniasyncCallback2 = new SNIAsyncCallback(this.WriteAsyncCallback);
				this._sessionHandle.SetAsyncCallbacks(sniasyncCallback, sniasyncCallback2);
			}
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00087572 File Offset: 0x00085772
		internal void ReadAsyncCallback(SNIPacket packet, uint error)
		{
			base.ReadAsyncCallback<SNIPacket>(IntPtr.Zero, packet, error);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00087581 File Offset: 0x00085781
		internal void WriteAsyncCallback(SNIPacket packet, uint sniError)
		{
			base.WriteAsyncCallback<SNIPacket>(IntPtr.Zero, packet, sniError);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x000094D4 File Offset: 0x000076D4
		protected override void RemovePacketFromPendingList(object packet)
		{
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00087590 File Offset: 0x00085790
		internal override void Dispose()
		{
			SNIPacket sniPacket = this._sniPacket;
			SNIHandle sessionHandle = this._sessionHandle;
			SNIPacket sniAsyncAttnPacket = this._sniAsyncAttnPacket;
			this._sniPacket = null;
			this._sessionHandle = null;
			this._sniAsyncAttnPacket = null;
			this._marsConnection = null;
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

		// Token: 0x06001B2A RID: 6954 RVA: 0x00087604 File Offset: 0x00085804
		internal override void DisposePacketCache()
		{
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._writePacketCache.Dispose();
			}
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000094D4 File Offset: 0x000076D4
		protected override void FreeGcHandle(int remaining, bool release)
		{
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0008764C File Offset: 0x0008584C
		internal override bool IsFailedHandle()
		{
			return this._sessionHandle.Status > 0U;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0008765C File Offset: 0x0008585C
		internal override object ReadSyncOverAsync(int timeoutRemaining, out uint error)
		{
			SNIHandle handle = this.Handle;
			if (handle == null)
			{
				throw ADP.ClosedConnectionError();
			}
			SNIPacket snipacket = null;
			error = SNIProxy.Singleton.ReadSyncOverAsync(handle, out snipacket, timeoutRemaining);
			return snipacket;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0008768C File Offset: 0x0008588C
		internal override bool IsPacketEmpty(object packet)
		{
			return packet == null;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x00087692 File Offset: 0x00085892
		internal override void ReleasePacket(object syncReadPacket)
		{
			((SNIPacket)syncReadPacket).Dispose();
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x000876A0 File Offset: 0x000858A0
		internal override uint CheckConnection()
		{
			SNIHandle handle = this.Handle;
			if (handle != null)
			{
				return SNIProxy.Singleton.CheckConnection(handle);
			}
			return 0U;
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x000876C4 File Offset: 0x000858C4
		internal override object ReadAsync(out uint error, ref object handle)
		{
			SNIPacket snipacket;
			error = SNIProxy.Singleton.ReadAsync((SNIHandle)handle, out snipacket);
			return snipacket;
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000876E8 File Offset: 0x000858E8
		internal override object CreateAndSetAttentionPacket()
		{
			if (this._sniAsyncAttnPacket == null)
			{
				SNIPacket snipacket = new SNIPacket();
				this.SetPacketData(snipacket, SQL.AttentionHeader, 8);
				this._sniAsyncAttnPacket = snipacket;
			}
			return this._sniAsyncAttnPacket;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0008771D File Offset: 0x0008591D
		internal override uint WritePacket(object packet, bool sync)
		{
			return SNIProxy.Singleton.WritePacket(this.Handle, (SNIPacket)packet, sync);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0000567E File Offset: 0x0000387E
		internal override object AddPacketToPendingList(object packet)
		{
			return packet;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00087736 File Offset: 0x00085936
		internal override bool IsValidPacket(object packetPointer)
		{
			return (SNIPacket)packetPointer != null && !((SNIPacket)packetPointer).IsInvalid;
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00087750 File Offset: 0x00085950
		internal override object GetResetWritePacket()
		{
			if (this._sniPacket != null)
			{
				this._sniPacket.Reset();
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

		// Token: 0x06001B37 RID: 6967 RVA: 0x000877BC File Offset: 0x000859BC
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

		// Token: 0x06001B38 RID: 6968 RVA: 0x0008781C File Offset: 0x00085A1C
		internal override void SetPacketData(object packet, byte[] buffer, int bytesUsed)
		{
			SNIProxy.Singleton.PacketSetData((SNIPacket)packet, buffer, bytesUsed);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00087830 File Offset: 0x00085A30
		internal override uint SniGetConnectionId(ref Guid clientConnectionId)
		{
			return SNIProxy.Singleton.GetConnectionId(this.Handle, ref clientConnectionId);
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00087843 File Offset: 0x00085A43
		internal override uint DisabeSsl()
		{
			return SNIProxy.Singleton.DisableSsl(this.Handle);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00087855 File Offset: 0x00085A55
		internal override uint EnableMars(ref uint info)
		{
			this._marsConnection = new SNIMarsConnection(this.Handle);
			if (this._marsConnection.StartReceive() == 997U)
			{
				return 0U;
			}
			return 1U;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0008787D File Offset: 0x00085A7D
		internal override uint EnableSsl(ref uint info)
		{
			return SNIProxy.Singleton.EnableSsl(this.Handle, info);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00087891 File Offset: 0x00085A91
		internal override uint SetConnectionBufferSize(ref uint unsignedPacketSize)
		{
			return SNIProxy.Singleton.SetConnectionBufferSize(this.Handle, unsignedPacketSize);
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000878A5 File Offset: 0x00085AA5
		internal override uint GenerateSspiClientContext(byte[] receivedBuff, uint receivedLength, ref byte[] sendBuff, ref uint sendLength, byte[] _sniSpnBuffer)
		{
			SNIProxy.Singleton.GenSspiClientContext(this.sspiClientContextStatus, receivedBuff, ref sendBuff, _sniSpnBuffer);
			sendLength = (uint)((sendBuff != null) ? sendBuff.Length : 0);
			return 0U;
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override uint WaitForSSLHandShakeToComplete()
		{
			return 0U;
		}

		// Token: 0x0400138D RID: 5005
		private SNIMarsConnection _marsConnection;

		// Token: 0x0400138E RID: 5006
		private SNIHandle _sessionHandle;

		// Token: 0x0400138F RID: 5007
		private SNIPacket _sniPacket;

		// Token: 0x04001390 RID: 5008
		internal SNIPacket _sniAsyncAttnPacket;

		// Token: 0x04001391 RID: 5009
		private readonly Dictionary<SNIPacket, SNIPacket> _pendingWritePackets = new Dictionary<SNIPacket, SNIPacket>();

		// Token: 0x04001392 RID: 5010
		private readonly TdsParserStateObjectManaged.WritePacketCache _writePacketCache = new TdsParserStateObjectManaged.WritePacketCache();

		// Token: 0x04001393 RID: 5011
		internal SspiClientContextStatus sspiClientContextStatus = new SspiClientContextStatus();

		// Token: 0x02000256 RID: 598
		internal sealed class WritePacketCache : IDisposable
		{
			// Token: 0x06001B40 RID: 6976 RVA: 0x000878CA File Offset: 0x00085ACA
			public WritePacketCache()
			{
				this._disposed = false;
				this._packets = new Stack<SNIPacket>();
			}

			// Token: 0x06001B41 RID: 6977 RVA: 0x000878E4 File Offset: 0x00085AE4
			public SNIPacket Take(SNIHandle sniHandle)
			{
				SNIPacket snipacket;
				if (this._packets.Count > 0)
				{
					snipacket = this._packets.Pop();
					snipacket.Reset();
				}
				else
				{
					snipacket = new SNIPacket();
				}
				return snipacket;
			}

			// Token: 0x06001B42 RID: 6978 RVA: 0x0008791A File Offset: 0x00085B1A
			public void Add(SNIPacket packet)
			{
				if (!this._disposed)
				{
					this._packets.Push(packet);
					return;
				}
				packet.Dispose();
			}

			// Token: 0x06001B43 RID: 6979 RVA: 0x00087937 File Offset: 0x00085B37
			public void Clear()
			{
				while (this._packets.Count > 0)
				{
					this._packets.Pop().Dispose();
				}
			}

			// Token: 0x06001B44 RID: 6980 RVA: 0x00087959 File Offset: 0x00085B59
			public void Dispose()
			{
				if (!this._disposed)
				{
					this._disposed = true;
					this.Clear();
				}
			}

			// Token: 0x04001394 RID: 5012
			private bool _disposed;

			// Token: 0x04001395 RID: 5013
			private Stack<SNIPacket> _packets;
		}
	}
}
