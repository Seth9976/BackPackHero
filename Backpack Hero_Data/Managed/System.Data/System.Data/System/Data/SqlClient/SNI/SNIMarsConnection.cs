using System;
using System.Collections.Generic;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000242 RID: 578
	internal class SNIMarsConnection
	{
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x000838B5 File Offset: 0x00081AB5
		public Guid ConnectionId
		{
			get
			{
				return this._connectionId;
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000838C0 File Offset: 0x00081AC0
		public SNIMarsConnection(SNIHandle lowerHandle)
		{
			this._lowerHandle = lowerHandle;
			this._lowerHandle.SetAsyncCallbacks(new SNIAsyncCallback(this.HandleReceiveComplete), new SNIAsyncCallback(this.HandleSendComplete));
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00083920 File Offset: 0x00081B20
		public SNIMarsHandle CreateMarsSession(object callbackObject, bool async)
		{
			SNIMarsHandle snimarsHandle2;
			lock (this)
			{
				ushort nextSessionId = this._nextSessionId;
				this._nextSessionId = nextSessionId + 1;
				ushort num = nextSessionId;
				SNIMarsHandle snimarsHandle = new SNIMarsHandle(this, num, callbackObject, async);
				this._sessions.Add((int)num, snimarsHandle);
				snimarsHandle2 = snimarsHandle;
			}
			return snimarsHandle2;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00083988 File Offset: 0x00081B88
		public uint StartReceive()
		{
			SNIPacket snipacket = null;
			if (this.ReceiveAsync(ref snipacket) == 997U)
			{
				return 997U;
			}
			return SNICommon.ReportSNIError(SNIProviders.SMUX_PROV, 0U, 19U, string.Empty);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000839BC File Offset: 0x00081BBC
		public uint Send(SNIPacket packet)
		{
			uint num;
			lock (this)
			{
				num = this._lowerHandle.Send(packet);
			}
			return num;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00083A00 File Offset: 0x00081C00
		public uint SendAsync(SNIPacket packet, SNIAsyncCallback callback)
		{
			uint num;
			lock (this)
			{
				num = this._lowerHandle.SendAsync(packet, false, callback);
			}
			return num;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00083A48 File Offset: 0x00081C48
		public uint ReceiveAsync(ref SNIPacket packet)
		{
			uint num;
			lock (this)
			{
				num = this._lowerHandle.ReceiveAsync(ref packet);
			}
			return num;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00083A8C File Offset: 0x00081C8C
		public uint CheckConnection()
		{
			uint num;
			lock (this)
			{
				num = this._lowerHandle.CheckConnection();
			}
			return num;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00083AD0 File Offset: 0x00081CD0
		public void HandleReceiveError(SNIPacket packet)
		{
			foreach (SNIMarsHandle snimarsHandle in this._sessions.Values)
			{
				snimarsHandle.HandleReceiveError(packet);
			}
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00083B28 File Offset: 0x00081D28
		public void HandleSendComplete(SNIPacket packet, uint sniErrorCode)
		{
			packet.InvokeCompletionCallback(sniErrorCode);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00083B34 File Offset: 0x00081D34
		public void HandleReceiveComplete(SNIPacket packet, uint sniErrorCode)
		{
			SNISMUXHeader snismuxheader = null;
			SNIPacket snipacket = null;
			SNIMarsHandle snimarsHandle = null;
			if (sniErrorCode != 0U)
			{
				SNIMarsConnection snimarsConnection = this;
				lock (snimarsConnection)
				{
					this.HandleReceiveError(packet);
					return;
				}
			}
			for (;;)
			{
				SNIMarsConnection snimarsConnection = this;
				lock (snimarsConnection)
				{
					if (this._currentHeaderByteCount != 16)
					{
						snismuxheader = null;
						snipacket = null;
						snimarsHandle = null;
						while (this._currentHeaderByteCount != 16)
						{
							int num = packet.TakeData(this._headerBytes, this._currentHeaderByteCount, 16 - this._currentHeaderByteCount);
							this._currentHeaderByteCount += num;
							if (num == 0)
							{
								sniErrorCode = this.ReceiveAsync(ref packet);
								if (sniErrorCode == 997U)
								{
									return;
								}
								this.HandleReceiveError(packet);
								return;
							}
						}
						this._currentHeader = new SNISMUXHeader
						{
							SMID = this._headerBytes[0],
							flags = this._headerBytes[1],
							sessionId = BitConverter.ToUInt16(this._headerBytes, 2),
							length = BitConverter.ToUInt32(this._headerBytes, 4) - 16U,
							sequenceNumber = BitConverter.ToUInt32(this._headerBytes, 8),
							highwater = BitConverter.ToUInt32(this._headerBytes, 12)
						};
						this._dataBytesLeft = (int)this._currentHeader.length;
						this._currentPacket = new SNIPacket((int)this._currentHeader.length);
					}
					snismuxheader = this._currentHeader;
					snipacket = this._currentPacket;
					if (this._currentHeader.flags == 8 && this._dataBytesLeft > 0)
					{
						int num2 = packet.TakeData(this._currentPacket, this._dataBytesLeft);
						this._dataBytesLeft -= num2;
						if (this._dataBytesLeft > 0)
						{
							sniErrorCode = this.ReceiveAsync(ref packet);
							if (sniErrorCode == 997U)
							{
								break;
							}
							this.HandleReceiveError(packet);
							break;
						}
					}
					this._currentHeaderByteCount = 0;
					if (!this._sessions.ContainsKey((int)this._currentHeader.sessionId))
					{
						SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.SMUX_PROV, 0U, 5U, string.Empty);
						this.HandleReceiveError(packet);
						this._lowerHandle.Dispose();
						this._lowerHandle = null;
						break;
					}
					if (this._currentHeader.flags == 4)
					{
						this._sessions.Remove((int)this._currentHeader.sessionId);
					}
					else
					{
						snimarsHandle = this._sessions[(int)this._currentHeader.sessionId];
					}
				}
				if (snismuxheader.flags == 8)
				{
					snimarsHandle.HandleReceiveComplete(snipacket, snismuxheader);
				}
				if (this._currentHeader.flags == 2)
				{
					try
					{
						snimarsHandle.HandleAck(snismuxheader.highwater);
					}
					catch (Exception ex)
					{
						SNICommon.ReportSNIError(SNIProviders.SMUX_PROV, 35U, ex);
					}
				}
				snimarsConnection = this;
				lock (snimarsConnection)
				{
					if (packet.DataLeft != 0)
					{
						continue;
					}
					sniErrorCode = this.ReceiveAsync(ref packet);
					if (sniErrorCode != 997U)
					{
						this.HandleReceiveError(packet);
					}
				}
				break;
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00083E7C File Offset: 0x0008207C
		public uint EnableSsl(uint options)
		{
			return this._lowerHandle.EnableSsl(options);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00083E8A File Offset: 0x0008208A
		public void DisableSsl()
		{
			this._lowerHandle.DisableSsl();
		}

		// Token: 0x040012F7 RID: 4855
		private readonly Guid _connectionId = Guid.NewGuid();

		// Token: 0x040012F8 RID: 4856
		private readonly Dictionary<int, SNIMarsHandle> _sessions = new Dictionary<int, SNIMarsHandle>();

		// Token: 0x040012F9 RID: 4857
		private readonly byte[] _headerBytes = new byte[16];

		// Token: 0x040012FA RID: 4858
		private SNIHandle _lowerHandle;

		// Token: 0x040012FB RID: 4859
		private ushort _nextSessionId;

		// Token: 0x040012FC RID: 4860
		private int _currentHeaderByteCount;

		// Token: 0x040012FD RID: 4861
		private int _dataBytesLeft;

		// Token: 0x040012FE RID: 4862
		private SNISMUXHeader _currentHeader;

		// Token: 0x040012FF RID: 4863
		private SNIPacket _currentPacket;
	}
}
