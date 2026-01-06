using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000243 RID: 579
	internal class SNIMarsHandle : SNIHandle
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x00083E97 File Offset: 0x00082097
		public override Guid ConnectionId
		{
			get
			{
				return this._connectionId;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x00083E9F File Offset: 0x0008209F
		public override uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00083EA8 File Offset: 0x000820A8
		public override void Dispose()
		{
			try
			{
				this.SendControlPacket(SNISMUXFlags.SMUX_FIN);
			}
			catch (Exception ex)
			{
				SNICommon.ReportSNIError(SNIProviders.SMUX_PROV, 35U, ex);
				throw;
			}
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00083EDC File Offset: 0x000820DC
		public SNIMarsHandle(SNIMarsConnection connection, ushort sessionId, object callbackObject, bool async)
		{
			this._sessionId = sessionId;
			this._connection = connection;
			this._callbackObject = callbackObject;
			this.SendControlPacket(SNISMUXFlags.SMUX_SYN);
			this._status = 0U;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00083F74 File Offset: 0x00082174
		private void SendControlPacket(SNISMUXFlags flags)
		{
			byte[] array = null;
			lock (this)
			{
				this.GetSMUXHeaderBytes(0, (byte)flags, ref array);
			}
			SNIPacket snipacket = new SNIPacket();
			snipacket.SetData(array, 16);
			this._connection.Send(snipacket);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00083FD4 File Offset: 0x000821D4
		private void GetSMUXHeaderBytes(int length, byte flags, ref byte[] headerBytes)
		{
			headerBytes = new byte[16];
			this._currentHeader.SMID = 83;
			this._currentHeader.flags = flags;
			this._currentHeader.sessionId = this._sessionId;
			this._currentHeader.length = (uint)(16 + length);
			SNISMUXHeader currentHeader = this._currentHeader;
			uint num;
			if (flags != 4 && flags != 2)
			{
				uint sequenceNumber = this._sequenceNumber;
				this._sequenceNumber = sequenceNumber + 1U;
				num = sequenceNumber;
			}
			else
			{
				num = this._sequenceNumber - 1U;
			}
			currentHeader.sequenceNumber = num;
			this._currentHeader.highwater = this._receiveHighwater;
			this._receiveHighwaterLastAck = this._currentHeader.highwater;
			BitConverter.GetBytes((short)this._currentHeader.SMID).CopyTo(headerBytes, 0);
			BitConverter.GetBytes((short)this._currentHeader.flags).CopyTo(headerBytes, 1);
			BitConverter.GetBytes(this._currentHeader.sessionId).CopyTo(headerBytes, 2);
			BitConverter.GetBytes(this._currentHeader.length).CopyTo(headerBytes, 4);
			BitConverter.GetBytes(this._currentHeader.sequenceNumber).CopyTo(headerBytes, 8);
			BitConverter.GetBytes(this._currentHeader.highwater).CopyTo(headerBytes, 12);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00084104 File Offset: 0x00082304
		private SNIPacket GetSMUXEncapsulatedPacket(SNIPacket packet)
		{
			uint sequenceNumber = this._sequenceNumber;
			byte[] array = null;
			this.GetSMUXHeaderBytes(packet.Length, 8, ref array);
			SNIPacket snipacket = new SNIPacket(16 + packet.Length);
			snipacket.Description = string.Format("({0}) SMUX packet {1}", (packet.Description == null) ? "" : packet.Description, sequenceNumber);
			snipacket.AppendData(array, 16);
			snipacket.AppendPacket(packet);
			return snipacket;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00084174 File Offset: 0x00082374
		public override uint Send(SNIPacket packet)
		{
			for (;;)
			{
				SNIMarsHandle snimarsHandle = this;
				lock (snimarsHandle)
				{
					if (this._sequenceNumber < this._sendHighwater)
					{
						break;
					}
				}
				this._ackEvent.Wait();
				snimarsHandle = this;
				lock (snimarsHandle)
				{
					this._ackEvent.Reset();
					continue;
				}
				break;
			}
			return this._connection.Send(this.GetSMUXEncapsulatedPacket(packet));
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00084208 File Offset: 0x00082408
		private uint InternalSendAsync(SNIPacket packet, SNIAsyncCallback callback)
		{
			uint num;
			lock (this)
			{
				if (this._sequenceNumber >= this._sendHighwater)
				{
					num = 1048576U;
				}
				else
				{
					SNIPacket smuxencapsulatedPacket = this.GetSMUXEncapsulatedPacket(packet);
					if (callback != null)
					{
						smuxencapsulatedPacket.SetCompletionCallback(callback);
					}
					else
					{
						smuxencapsulatedPacket.SetCompletionCallback(new SNIAsyncCallback(this.HandleSendComplete));
					}
					num = this._connection.SendAsync(smuxencapsulatedPacket, callback);
				}
			}
			return num;
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0008428C File Offset: 0x0008248C
		private uint SendPendingPackets()
		{
			for (;;)
			{
				lock (this)
				{
					if (this._sequenceNumber < this._sendHighwater)
					{
						if (this._sendPacketQueue.Count != 0)
						{
							SNIMarsQueuedPacket snimarsQueuedPacket = this._sendPacketQueue.Peek();
							uint num = this.InternalSendAsync(snimarsQueuedPacket.Packet, snimarsQueuedPacket.Callback);
							if (num != 0U && num != 997U)
							{
								return num;
							}
							this._sendPacketQueue.Dequeue();
							continue;
						}
						else
						{
							this._ackEvent.Set();
						}
					}
				}
				break;
			}
			return 0U;
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0008432C File Offset: 0x0008252C
		public override uint SendAsync(SNIPacket packet, bool disposePacketAfterSendAsync, SNIAsyncCallback callback = null)
		{
			lock (this)
			{
				this._sendPacketQueue.Enqueue(new SNIMarsQueuedPacket(packet, (callback != null) ? callback : new SNIAsyncCallback(this.HandleSendComplete)));
			}
			this.SendPendingPackets();
			return 997U;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00084390 File Offset: 0x00082590
		public override uint ReceiveAsync(ref SNIPacket packet)
		{
			Queue<SNIPacket> receivedPacketQueue = this._receivedPacketQueue;
			lock (receivedPacketQueue)
			{
				int count = this._receivedPacketQueue.Count;
				if (this._connectionError != null)
				{
					return SNICommon.ReportSNIError(this._connectionError);
				}
				if (count == 0)
				{
					this._asyncReceives++;
					return 997U;
				}
				packet = this._receivedPacketQueue.Dequeue();
				if (count == 1)
				{
					this._packetEvent.Reset();
				}
			}
			lock (this)
			{
				this._receiveHighwater += 1U;
			}
			this.SendAckIfNecessary();
			return 0U;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00084460 File Offset: 0x00082660
		public void HandleReceiveError(SNIPacket packet)
		{
			Queue<SNIPacket> receivedPacketQueue = this._receivedPacketQueue;
			lock (receivedPacketQueue)
			{
				this._connectionError = SNILoadHandle.SingletonInstance.LastError;
				this._packetEvent.Set();
			}
			((TdsParserStateObject)this._callbackObject).ReadAsyncCallback<SNIPacket>(packet, 1U);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x000844C8 File Offset: 0x000826C8
		public void HandleSendComplete(SNIPacket packet, uint sniErrorCode)
		{
			lock (this)
			{
				((TdsParserStateObject)this._callbackObject).WriteAsyncCallback<SNIPacket>(packet, sniErrorCode);
			}
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00084510 File Offset: 0x00082710
		public void HandleAck(uint highwater)
		{
			lock (this)
			{
				if (this._sendHighwater != highwater)
				{
					this._sendHighwater = highwater;
					this.SendPendingPackets();
				}
			}
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0008455C File Offset: 0x0008275C
		public void HandleReceiveComplete(SNIPacket packet, SNISMUXHeader header)
		{
			SNIMarsHandle snimarsHandle = this;
			lock (snimarsHandle)
			{
				if (this._sendHighwater != header.highwater)
				{
					this.HandleAck(header.highwater);
				}
				Queue<SNIPacket> receivedPacketQueue = this._receivedPacketQueue;
				lock (receivedPacketQueue)
				{
					if (this._asyncReceives == 0)
					{
						this._receivedPacketQueue.Enqueue(packet);
						this._packetEvent.Set();
						return;
					}
					this._asyncReceives--;
					((TdsParserStateObject)this._callbackObject).ReadAsyncCallback<SNIPacket>(packet, 0U);
				}
			}
			snimarsHandle = this;
			lock (snimarsHandle)
			{
				this._receiveHighwater += 1U;
			}
			this.SendAckIfNecessary();
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0008464C File Offset: 0x0008284C
		private void SendAckIfNecessary()
		{
			uint receiveHighwater;
			uint receiveHighwaterLastAck;
			lock (this)
			{
				receiveHighwater = this._receiveHighwater;
				receiveHighwaterLastAck = this._receiveHighwaterLastAck;
			}
			if (receiveHighwater - receiveHighwaterLastAck > 2U)
			{
				this.SendControlPacket(SNISMUXFlags.SMUX_ACK);
			}
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0008469C File Offset: 0x0008289C
		public override uint Receive(out SNIPacket packet, int timeoutInMilliseconds)
		{
			packet = null;
			uint num = 997U;
			for (;;)
			{
				Queue<SNIPacket> receivedPacketQueue = this._receivedPacketQueue;
				lock (receivedPacketQueue)
				{
					if (this._connectionError != null)
					{
						return SNICommon.ReportSNIError(this._connectionError);
					}
					int count = this._receivedPacketQueue.Count;
					if (count > 0)
					{
						packet = this._receivedPacketQueue.Dequeue();
						if (count == 1)
						{
							this._packetEvent.Reset();
						}
						num = 0U;
					}
				}
				if (num == 0U)
				{
					break;
				}
				if (!this._packetEvent.Wait(timeoutInMilliseconds))
				{
					goto Block_4;
				}
			}
			lock (this)
			{
				this._receiveHighwater += 1U;
			}
			this.SendAckIfNecessary();
			return num;
			Block_4:
			SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.SMUX_PROV, 0U, 11U, string.Empty);
			return 258U;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00084798 File Offset: 0x00082998
		public override uint CheckConnection()
		{
			return this._connection.CheckConnection();
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000094D4 File Offset: 0x000076D4
		public override void SetAsyncCallbacks(SNIAsyncCallback receiveCallback, SNIAsyncCallback sendCallback)
		{
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000094D4 File Offset: 0x000076D4
		public override void SetBufferSize(int bufferSize)
		{
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000847A5 File Offset: 0x000829A5
		public override uint EnableSsl(uint options)
		{
			return this._connection.EnableSsl(options);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000847B3 File Offset: 0x000829B3
		public override void DisableSsl()
		{
			this._connection.DisableSsl();
		}

		// Token: 0x04001300 RID: 4864
		private const uint ACK_THRESHOLD = 2U;

		// Token: 0x04001301 RID: 4865
		private readonly SNIMarsConnection _connection;

		// Token: 0x04001302 RID: 4866
		private readonly uint _status = uint.MaxValue;

		// Token: 0x04001303 RID: 4867
		private readonly Queue<SNIPacket> _receivedPacketQueue = new Queue<SNIPacket>();

		// Token: 0x04001304 RID: 4868
		private readonly Queue<SNIMarsQueuedPacket> _sendPacketQueue = new Queue<SNIMarsQueuedPacket>();

		// Token: 0x04001305 RID: 4869
		private readonly object _callbackObject;

		// Token: 0x04001306 RID: 4870
		private readonly Guid _connectionId = Guid.NewGuid();

		// Token: 0x04001307 RID: 4871
		private readonly ushort _sessionId;

		// Token: 0x04001308 RID: 4872
		private readonly ManualResetEventSlim _packetEvent = new ManualResetEventSlim(false);

		// Token: 0x04001309 RID: 4873
		private readonly ManualResetEventSlim _ackEvent = new ManualResetEventSlim(false);

		// Token: 0x0400130A RID: 4874
		private readonly SNISMUXHeader _currentHeader = new SNISMUXHeader();

		// Token: 0x0400130B RID: 4875
		private uint _sendHighwater = 4U;

		// Token: 0x0400130C RID: 4876
		private int _asyncReceives;

		// Token: 0x0400130D RID: 4877
		private uint _receiveHighwater = 4U;

		// Token: 0x0400130E RID: 4878
		private uint _receiveHighwaterLastAck = 4U;

		// Token: 0x0400130F RID: 4879
		private uint _sequenceNumber;

		// Token: 0x04001310 RID: 4880
		private SNIError _connectionError;
	}
}
